using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EcommApi.api.DataContext;
using EcommApi.api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EcommApi.api.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;
        private readonly jwtOptions _options;
        public AuthController(AppDbContext context, IOptions<jwtOptions> options)
        {
            _context = context;
            _options = options.Value;
            
        }

        [HttpPost("Login")]
        public async Task<IActionResult> loginUser([FromBody] UserLogin userlogin)
        {
            if (userlogin == null)
            {
                return BadRequest();
            }

            var Username = await _context.Users.FirstOrDefaultAsync(s => s.Username == userlogin.Username);

            if (Username == null)
            {
                return BadRequest("Username not registered");
            }

            var dbPass = Username.Password;
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(userlogin.Password));
            userlogin.Password = Convert.ToBase64String(hash);

            if (dbPass != userlogin.Password)
            {
                return BadRequest("Incorrect email or pass");
            }
            //Console.WriteLine(userlogin.Username);
           var token = GetJWTToken(Username);
           return Ok(new { token = token });
            //return Ok(Username.Id);

        }

        private string GetJWTToken(User user)
        {
            var jwtKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
            var crendential = new SigningCredentials(jwtKey, SecurityAlgorithms.HmacSha256);
            List<Claim> claims = new List<Claim>()
            {
                //new Claim(ClaimTypes.SerialNumber, $"{user.Id}"),
                //new Claim(ClaimTypes.Role, user.Role),
                //new Claim(ClaimTypes.Name, )

                new Claim("Username",user.Username),
                new Claim("Email", user.Email),
                new Claim("Role",user.Role),
                new Claim("Id",$"{user.Id}")

               
                
                //new Claim("Id", )
            };
            var sToken = new JwtSecurityToken(_options.Key, _options.Issuer, claims, expires: DateTime.Now.AddHours(5), signingCredentials: crendential);
            var token = new JwtSecurityTokenHandler().WriteToken(sToken);
            return token;
        }
    }
}
