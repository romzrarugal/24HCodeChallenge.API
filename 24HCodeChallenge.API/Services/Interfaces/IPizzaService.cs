using _24HCodeChallenge.API.DTOs;
using _24HCodeChallenge.API.DTOs.Common;
using MyProject.Domain.Entities;

namespace _24HCodeChallenge.API.Services.Interfaces
{
    public interface IPizzaService : IPersistence<Pizza, string>
    {
        Task<PagedResult<PizzaSummaryDto>> GetPizzaSummariesAsync(string? search, List<string> ingredients, int page, int pageSize, string sortby, string sortDirection);
        Task<PizzaInsightDto?> GetPizzaInsightAsync(string pizzaTypeId);
        Task<List<string>> GetAllIngredientsAsync();
    }
}
