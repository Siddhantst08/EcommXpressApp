using System.Text;
using EcommApi.api.Models;
using EcommApi.api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommApi.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        /*

        [HttpPost("Login")]
        public async Task<IActionResult> CheckLogin([FromBody] Auth userlogin)
        {
            if (userlogin == null)
            {
                return BadRequest();
            }

            var flag = await _userService.AuthenticateUserAsync(userlogin);

            var user = await _context.UserAuth.FirstOrDefaultAsync(s => s.Username == userlogin.Username);

            if (user == null)
            {
                return BadRequest("username not registered");
            }

            var dbPass = user.Password;
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(userlogin.Password));
            userlogin.Password = Convert.ToBase64String(hash);

            if (dbPass != userlogin.Password)
            {
                return BadRequest("Incorrect email or pass");
            }
            //Console.WriteLine(userlogin.username);
            var token = GetJWTToken(userlogin.Username);
            return Ok(new { token = token });

        }
    */
     

        [HttpGet]
        public async Task<IActionResult> GetUsers() {
            return Ok(await _userService.GetUserAsync());
        }

        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {

            if (user == null)
            {
                return BadRequest("Please enter valid data");
            }
            bool flag = await _userService.PostUserAsync(user);

            if (flag)
            {
                return Ok("User added successfully");
            }
            return BadRequest("User already exist");
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetUserById([FromQuery] int id)
        {
            var prod = await _userService.GetUserByIdAsync(id);
            if(prod == null)
            {
                return NotFound("User Not Found");
            }
            return Ok(prod);
        }
        [HttpDelete("id")]
        public async Task<IActionResult> DelUser([FromQuery] int id)
        {
            var flag = await _userService.DelUserAsync(id);
            if(flag)
            {
                return Ok("User Deleted Succesfully");
            }
            return NotFound("User not found");
        }

        [HttpPut]
        public async Task<IActionResult> EditUser([FromBody] User user, [FromQuery] int id)
        {
            var flag = await _userService.EditUserAsync(user, id);
            if (flag)
            {
                return Ok("User Details updated Successfully");
            }
            else return BadRequest("User details already exists");
        }


    }
}
