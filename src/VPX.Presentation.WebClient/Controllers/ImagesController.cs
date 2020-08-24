using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VPX.Presentation.WebClient.Infrastructure.Managers.Uploader;

namespace VPX.Presentation.WebClient.Controllers
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
                var image = await FileUploader.UploadToBase64(file);
                return Ok(new { image });
            }

            return BadRequest();
        }
    }
}