using _24HCodeChallenge.API.DTOs;
using _24HCodeChallenge.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using MyProject.Data;

namespace _24HCodeChallenge.API.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly AppDbContext _context;

        public DashboardService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardSummaryDto> GetDashboardSummaryAsync()
        {
            var orderDetails = await _context.OrderDetails
                .Include(od => od.Pizza)
                .ThenInclude(p => p.PizzaType)
                .ToListAsync();

            var totalSales = orderDetails.Sum(od => od.Quantity * od.Pizza.Price);
            var totalQuantity = orderDetails.Sum(od => od.Quantity);
            var totalOrders = await _context.Orders.CountAsync();

            var bestSellers = orderDetails
                .GroupBy(od => new { od.Pizza.PizzaType.Category, od.Pizza.PizzaType })
                .Select(g => new
                {
                    g.Key.Category,
                    g.Key.PizzaType,
                    Quantity = g.Sum(x => x.Quantity),
                    Sales = g.Sum(x => x.Quantity * x.Pizza.Price)
                })
                .GroupBy(x => x.Category)
                .Select(g => g.OrderByDescending(x => x.Quantity).First())
                .Select(x => new BestSellerDto
                {
                    Category = $"Category: {x.Category}",
                    PizzaName = x.PizzaType.Name,
                    QuantitySold = x.Quantity,
                    TotalSales = x.Sales
                })
                .ToList();


            return new DashboardSummaryDto
            {
                TotalSales = totalSales,
                TotalQuantitySold = totalQuantity,
                TotalOrders = totalOrders,
                BestSellers = bestSellers
            };
        }

        public async Task<List<PizzaChartDataDto>> GetChartDataAsync()
        {
            var orderDetails = await _context.OrderDetails
                .Include(od => od.Pizza)
                .ThenInclude(p => p.PizzaType)
                .ToListAsync();

            var groupedByCategory = orderDetails
                .GroupBy(od => new { od.Pizza.PizzaType.Category, od.Pizza.PizzaType.Name })
                .Select(g => new
                {
                    g.Key.Category,
                    g.Key.Name,
                    Quantity = g.Sum(x => x.Quantity),
                    Sales = g.Sum(x => x.Quantity * x.Pizza.Price)
                })
                .GroupBy(x => x.Category)
                .Select(g => new PizzaChartDataDto
                {
                    Category = g.Key,
                    Pizzas = g.Select(x => new PizzaChartItemDto
                    {
                        PizzaName = x.Name,
                        QuantitySold = x.Quantity,
                        TotalSales = x.Sales
                    }).ToList()
                })
                .ToList();

            return groupedByCategory;
        }

    }

}
