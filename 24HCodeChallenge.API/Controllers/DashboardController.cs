using _24HCodeChallenge.API.DTOs;
using _24HCodeChallenge.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _24HCodeChallenge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        /// <summary>
        /// Gets the Dashboard Summary (Total Sales, Total Pizzas Sold, Total Orders and Best Sellers per Category)
        /// </summary>
        /// <returns></returns>
        [HttpGet("summary")]
        public async Task<ActionResult<DashboardSummaryDto>> GetDashboardSummary()
        {
            var summary = await _dashboardService.GetDashboardSummaryAsync();
            return Ok(summary);
        }

        /// <summary>
        /// Gets the Chart Data for each Category and Pizza
        /// </summary>
        /// <returns></returns>
        [HttpGet("charts")]
        public async Task<ActionResult<List<PizzaChartDataDto>>> GetChartData()
        {
            var chartData = await _dashboardService.GetChartDataAsync();
            return Ok(chartData);
        }
    }
}
