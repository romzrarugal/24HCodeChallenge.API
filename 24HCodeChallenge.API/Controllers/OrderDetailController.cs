using _24HCodeChallenge.API.Services;
using _24HCodeChallenge.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _24HCodeChallenge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;

        public OrderDetailController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        // GET: api/orderdetail
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orderDetails = await _orderDetailService.GetAllAsync();
            return Ok(orderDetails);
        }

        // GET: api/orderdetail/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var orderDetail = await _orderDetailService.GetByIdAsync(id);
            if (orderDetail == null)
                return NotFound();

            return Ok(orderDetail);
        }
    }
}
