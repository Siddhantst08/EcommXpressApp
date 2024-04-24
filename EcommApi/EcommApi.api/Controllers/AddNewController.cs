using EcommApi.api.DataContext;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace EcommApi.api.Controllers
{
    public class AddNewController : Controller
    {
        private readonly AppDbContext _context;
        [HttpPost("fileupload"), DisableRequestSizeLimit]
        public async Task<IActionResult> Upload()
        {
            try
            {
                // var file = Request.Form.Files[0];
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    dynamic fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { dbPath, formCollection });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
    
