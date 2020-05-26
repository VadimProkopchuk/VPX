using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JML.Presentation.WebClient.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Upload([FromForm] IFormFile file)
        {
            if (file.Length > 0)
            {
                using var stream = new MemoryStream();

                await file.CopyToAsync(stream);
                var base64 = Convert.ToBase64String(stream.ToArray());
                var mime = file.ContentType;
                var image = $"data:{mime};base64,{base64}";

                return Ok(new { image });
            }

            return BadRequest();
        }
    }
}