using _24HCodeChallenge.API.DTOs;

namespace _24HCodeChallenge.API.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardSummaryDto> GetDashboardSummaryAsync();
        Task<List<PizzaChartDataDto>> GetChartDataAsync();
    }
}
