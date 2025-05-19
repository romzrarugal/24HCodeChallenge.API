using _24HCodeChallenge.API.DTOs;
using _24HCodeChallenge.API.Services;
using _24HCodeChallenge.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _24HCodeChallenge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly IPizzaService _pizzaService;

        public PizzaController(IPizzaService pizzaService)
        {
            _pizzaService = pizzaService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var pizza = await _pizzaService.GetByIdAsync(id);
            if (pizza == null)
                return NotFound();

            return Ok(pizza);
        }

        /// <summary>
        /// Gets a paginated list of pizza summaries with optional search, filtering, and sorting.
        /// </summary>
        /// <param name="search">Optional search term for pizza name.</param>
        /// <param name="page">Page number (default is 1).</param>
        /// <param name="pageSize">Number of items per page (default is 10).</param>
        /// <param name="ingredients">List of ingredients to filter by.</param>
        /// <param name="sortBy">Field to sort by (default is 'name').</param>
        /// <param name="sortDirection">Sort direction: 'asc' or 'desc'.</param>
        /// <returns>Paginated list of pizza summaries.</returns>
        [HttpGet]
        public async Task<IActionResult> GetPizzaSummaries(
            [FromQuery] string? search,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? ingredients = null,
            [FromQuery] string sortBy = "name",
            [FromQuery] string sortDirection = "asc")
        {
            var ingredientList = string.IsNullOrWhiteSpace(ingredients)
                ? new List<string>()
                : ingredients.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(i => i.Trim()).ToList();

            var result = await _pizzaService.GetPizzaSummariesAsync(search, ingredientList, page, pageSize, sortBy, sortDirection);
            if (result == null || result.Items == null || !result.Items.Any())
                return NotFound("No pizzas found matching the criteria.");

            return Ok(result);
        }

        /// <summary>
        /// Gets the insight of a specific pizza type to be used in the Sales Summary charts
        /// </summary>
        /// <param name="id">Pizza Type Id</param>
        /// <returns>Detailed Insight/Sales Summary of a Pizza Type</returns>
        [HttpGet("{id}/insight")]
        public async Task<IActionResult> GetPizzaInsight(string id)
        {
            var result = await _pizzaService.GetPizzaInsightAsync(id);

            if (result == null)
                return NotFound("No insight available.");

            return Ok(result);
        }

        /// <summary>
        /// Gets the list of each Ingredients
        /// </summary>
        /// <returns></returns>
        [HttpGet("ingredients")]
        public async Task<IActionResult> GetIngredients()
        {
            var ingredients = await _pizzaService.GetAllIngredientsAsync();
            if (ingredients == null || !ingredients.Any())
                return NotFound("No ingredients found.");

            return Ok(ingredients);
        }
    }
}
