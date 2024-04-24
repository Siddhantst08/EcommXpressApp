using EcommApi.api.Models;
using EcommApi.api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommApi.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {   
        private readonly MailService _mailService;

        public MailController(MailService mailService)
        {
            _mailService = mailService;
        }
        [HttpPost]
        public async Task<IActionResult> PostMail([FromBody] Mail mail) {
            if (mail == null)
            {
                return BadRequest();
            }
            var receiver = mail.Email;
            var subject = mail.Subject;
            var message = mail.Body;
           // var subject = "Order Placed!!";
           // var message = "Congrats! Your order with EcommXpress has been Placed Successfully.";

            await _mailService.SendEmailAsync(receiver, subject, message);

            return Ok("Email Sent");
        }
    }
}
