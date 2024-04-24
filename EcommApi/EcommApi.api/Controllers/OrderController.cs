using EcommApi.api.Models;
using EcommApi.api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommApi.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrder()
        {
            return Ok(await _orderService.GetOrderAsync());
        }


        [HttpPost]
        public async Task<IActionResult> PostOrder([FromBody] Order order)
        {

            if (order == null)
            {
                return BadRequest("Please enter valid data");
            }
            bool flag = await _orderService.PostOrderAsync(order);

            if (flag)
            {
                return Ok("Order added successfully");
            }
            return BadRequest("Order already exist");
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetOrderById([FromQuery] int id)
        {
            var prod = await _orderService.GetOrderByIdAsync(id);
            if (prod == null)
            {
                return NotFound("Order Not Found");
            }
            return Ok(prod);
        }
        [HttpDelete("id")]
        public async Task<IActionResult> DelOrder([FromQuery] int id)
        {
            var flag = await _orderService.DelOrderAsync(id);
            if (flag)
            {
                return Ok("Order Deleted Succesfully");
            }
            return NotFound("Order not found");
        }

        [HttpPut]
        public async Task<IActionResult> EditOrder([FromBody] Order order, [FromQuery] int id)
        {
            var flag = await _orderService.EditOrderAsync(order, id);
            if (flag)
            {
                return Ok("Order details updated Successfully");
            }
            else return BadRequest("Order details already exists");
        }


    }
}
