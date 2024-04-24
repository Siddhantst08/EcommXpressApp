using EcommApi.api.Models;
using EcommApi.api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommApi.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService) { 
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategory()
        {
            return Ok(await _categoryService.GetCategoryAsync());
        }


        [HttpPost]
        public async Task<IActionResult> PostCategory([FromBody] Category category)
        {

            if (category == null)
            {
                return BadRequest("Please enter valid data");
            }
            bool flag = await _categoryService.PostCategoryAsync(category);

            if (flag)
            {
                return Ok("Category added successfully");
            }
            return BadRequest("Category already exist");
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetCategoryById([FromQuery] int id)
        {
            var prod = await _categoryService.GetCategoryByIdAsync(id);
            if (prod == null)
            {
                return NotFound("Category Not Found");
            }
            return Ok(prod);
        }
        [HttpDelete("id")]
        public async Task<IActionResult> DelCategory([FromQuery] int id)
        {
            var flag = await _categoryService.DelCategoryAsync(id);
            if (flag)
            {
                return Ok("Category Deleted Succesfully");
            }
            return NotFound("Category not found");
        }

        [HttpPut]
        public async Task<IActionResult> EditUser([FromBody] Category category, [FromQuery] int id)
        {
            var flag = await _categoryService.EditCategoryAsync(category, id);
            if (flag)
            {
                return Ok("Category name updated Successfully");
            }
            else return BadRequest("Category details already exists");
        }




    }
}
