using EcommApi.api.Models;
using EcommApi.api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommApi.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        public readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            //return Ok();
            return Ok(await _cartService.GetCartAsync());
        }


        [HttpPost]
        public async Task<IActionResult> PostCart([FromBody] Cart Cart)
        {

            if (Cart == null)
            {
                return BadRequest("Please enter valid data");
            }
            bool flag = await _cartService.PostCartAsync(Cart);

            if (flag)
            {
                return Ok("Cart added successfully");
            }
            return BadRequest("Cart already exist");
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetCartById([FromQuery] int id)
        {
            var prod = await _cartService.GetCartByIdAsync(id);
            if (prod == null)
            {
                return NotFound("Cart Not Found");
            }
            return Ok(prod);
        }
        [HttpDelete("id")]
        public async Task<IActionResult> DelCart([FromQuery] int id)
        {
            var flag = await _cartService.DelCartAsync(id);
            if (flag)
            {
                return Ok("Cart Deleted Succesfully");
            }
            return NotFound("Cart not found");
        }

        [HttpPut]
        public async Task<IActionResult> EditCart([FromBody] Cart Cart, [FromQuery] int id)
        {
            var flag = await _cartService.EditCartAsync(Cart, id);
            if (flag)
            {
                return Ok("Cart details updated Successfully");
            }
            else return BadRequest("Cart details already exists");
        }


    }
}
