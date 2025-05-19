using _24HCodeChallenge.API.DTOs;
using _24HCodeChallenge.API.DTOs.Common;
using _24HCodeChallenge.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using MyProject.Data;
using MyProject.Domain.Entities;
using System.Globalization;

namespace _24HCodeChallenge.API.Services
{
    public class PizzaService : IPizzaService
    {
        private readonly AppDbContext _context;
        private readonly IPersistence<Pizza, string> _persistence;
        public PizzaService(AppDbContext context, IPersistence<Pizza, string> persistence)
        {
            _context = context;
            _persistence = persistence;
        }
        public Task<IEnumerable<Pizza>> GetAllAsync() => _persistence.GetAllAsync();
        public Task<Pizza?> GetByIdAsync(string id) => _persistence.GetByIdAsync(id);

        public async Task<PagedResult<PizzaSummaryDto>> GetPizzaSummariesAsync(string? search, List<string> ingredients, int page, int pageSize, string sortBy, string sortDirection)
        {
            var pizzas = await _context.Pizzas
                .Include(p => p.PizzaType)
                .ToListAsync();

            var pizzaTypeMap = _context.PizzaTypes
                .ToDictionary(pt => pt.PizzaTypeId);

            var orderDetails = await _context.OrderDetails
                .Join(_context.Orders,
                      od => od.OrderId,
                      o => o.OrderId,
                      (od, o) => new { od.PizzaId, od.Quantity, o.Date })
                .ToListAsync();

            var pizzaMap = pizzas
                .GroupBy(p => p.PizzaTypeId)
                .ToDictionary(g => g.Key, g => g.ToList());

            var summaries = new List<PizzaSummaryDto>();

            foreach (var pizzaTypeId in pizzaMap.Keys)
            {
                if (!pizzaTypeMap.TryGetValue(pizzaTypeId, out var pizzaType))
                    continue;

                var allPizzas = pizzaMap[pizzaTypeId];

                int totalQty = 0;
                decimal totalSales = 0;

                foreach (var pizza in allPizzas)
                {
                    var matches = orderDetails.Where(o => o.PizzaId == pizza.PizzaId);
                    foreach (var match in matches)
                    {
                        totalQty += match.Quantity;
                        totalSales += match.Quantity * pizza.Price;
                    }
                }

                var dto = new PizzaSummaryDto
                {
                    PizzaId = pizzaType.PizzaTypeId,
                    Name = pizzaType.Name,
                    Category = pizzaType.Category,
                    Ingredients = pizzaType.Ingredients,
                    QuantitySold = totalQty,
                    TotalSales = totalSales
                };

                summaries.Add(dto);
            }

            // Filtering logic (search + ingredients)
            if (!string.IsNullOrWhiteSpace(search))
            {
                var lower = search.ToLower();
                summaries = summaries.Where(p =>
                    p.Name.ToLower().Contains(lower) ||
                    p.Category.ToLower().Contains(lower) ||
                    p.Ingredients.ToLower().Contains(lower)).ToList();
            }

            if (ingredients.Any())
            {
                var selectedSet = new HashSet<string>(
                    ingredients.Select(i => i.Trim().ToLower())
                );

                summaries = summaries
                    .Where(p =>
                    {
                        var pizzaIngredients = p.Ingredients
                            .Split(',', StringSplitOptions.RemoveEmptyEntries)
                            .Select(i => i.Trim().ToLower());

                        // If any selected ingredient is in this pizza's ingredients, exclude it
                        return !pizzaIngredients.Any(pi => selectedSet.Contains(pi));
                    })
                    .ToList();
            }

            var totalCount = summaries.Count;

            switch (sortBy.ToLower())
            {
                case "name":
                    summaries = sortDirection == "asc"
                        ? summaries.OrderBy(p => p.Name).ToList()
                        : summaries.OrderByDescending(p => p.Name).ToList();
                    break;

                case "category":
                    summaries = sortDirection == "asc"
                        ? summaries.OrderBy(p => p.Category).ToList()
                        : summaries.OrderByDescending(p => p.Category).ToList();
                    break;

                case "quantitysold":
                    summaries = sortDirection == "asc"
                        ? summaries.OrderBy(p => p.QuantitySold).ToList()
                        : summaries.OrderByDescending(p => p.QuantitySold).ToList();
                    break;

                case "totalsales":
                default:
                    summaries = sortDirection == "asc"
                        ? summaries.OrderBy(p => p.TotalSales).ToList()
                        : summaries.OrderByDescending(p => p.TotalSales).ToList();
                    break;
            }

            var paged = summaries
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedResult<PizzaSummaryDto>
            {
                TotalCount = totalCount,
                Items = paged
            };
        }

        public async Task<PizzaInsightDto?> GetPizzaInsightAsync(string pizzaTypeId)
        {
            var pizzas = await _context.Pizzas
                .Where(p => p.PizzaTypeId == pizzaTypeId)
                .ToListAsync();

            if (!pizzas.Any())
                return null;

            var pizzaIds = pizzas.Select(p => p.PizzaId).ToList();

            var orderDetails = await _context.OrderDetails
                .Where(od => pizzaIds.Contains(od.PizzaId))
                .Join(_context.Orders,
                      od => od.OrderId,
                      o => o.OrderId,
                      (od, o) => new
                      {
                          od.PizzaId,
                          od.Quantity,
                          o.Date
                      })
                .ToListAsync();

            // Build a lookup for price per pizzaId
            var priceMap = pizzas.ToDictionary(p => p.PizzaId, p => p.Price);
            var sizeMap = pizzas.ToDictionary(p => p.PizzaId, p => p.Size);

            var dto = new PizzaInsightDto();

            foreach (var detail in orderDetails)
            {
                var size = sizeMap[detail.PizzaId];
                var price = priceMap[detail.PizzaId];

                // Group by size
                if (!dto.SalesPerSize.ContainsKey(size))
                    dto.SalesPerSize[size] = 0;
                dto.SalesPerSize[size] += detail.Quantity * price;

                // Group by month
                var monthKey = detail.Date.ToString("yyyy-MM");
                if (!dto.MonthlySales.ContainsKey(monthKey))
                    dto.MonthlySales[monthKey] = 0;
                dto.MonthlySales[monthKey] += detail.Quantity * price;

                if (!dto.QuantityPerMonth.ContainsKey(monthKey))
                    dto.QuantityPerMonth[monthKey] = 0;
                dto.QuantityPerMonth[monthKey] += detail.Quantity;
            }

            return dto;
        }

        public async Task<List<string>> GetAllIngredientsAsync()
        {
            var ingredients = await _context.PizzaTypes
                .Select(p => p.Ingredients)
                .ToListAsync();

            return ingredients
                .SelectMany(i => i.Split(',', StringSplitOptions.RemoveEmptyEntries))
                .Select(i => i.Trim())
                .Where(i => !string.IsNullOrWhiteSpace(i))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(i => i)
                .ToList();
        }
    }
}
