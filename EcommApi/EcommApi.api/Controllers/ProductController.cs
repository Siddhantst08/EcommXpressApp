using EcommApi.api.Models;
using EcommApi.api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommApi.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {
            return Ok(await _productService.GetProductAsync());
        }


        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody] Product product)
        {

            if (product == null)
            {
                return BadRequest("Please enter valid data");
            }
            bool flag = await _productService.PostProductAsync(product);

            if (flag)
            {
                return Ok("Product added successfully");
            }
            return BadRequest("Product already exist");
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetProductById([FromQuery] int id)
        {
            var prod = await _productService.GetProductByIdAsync(id);
            if (prod == null)
            {
                return NotFound("Product Not Found");
            }
            return Ok(prod);
        }
        [HttpDelete("id")]
        public async Task<IActionResult> DelProduct([FromQuery] int id)
        {
            var flag = await _productService.DelProductAsync(id);
            if (flag)
            {
                return Ok("Product Deleted Succesfully");
            }
            return NotFound("Product not found");
        }

        [HttpPut]
        public async Task<IActionResult> EditProduct([FromBody] Product product, [FromQuery] int id)
        {
            var flag = await _productService.EditProductAsync(product, id);
            if (flag)
            {
                return Ok("Product details updated Successfully");
            }
            else return BadRequest("Product details already exists");
        }


    }
}
