using _24HCodeChallenge.API.Services;
using _24HCodeChallenge.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _24HCodeChallenge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaTypeController : ControllerBase
    {
        private readonly IPizzaTypeService _pizzaTypeService;

        public PizzaTypeController (IPizzaTypeService pizzaTypeService)
        {
            _pizzaTypeService = pizzaTypeService;
        }

        // GET: api/pizzatype
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pizzaTypes = await _pizzaTypeService.GetAllAsync();
            return Ok(pizzaTypes);
        }

        // GET: api/pizzatype/big_meat
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var pizzaType = await _pizzaTypeService.GetByIdAsync(id);
            if (pizzaType == null)
                return NotFound();

            return Ok(pizzaType);
        }
    }
}
