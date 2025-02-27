using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudinaryFramework.Services;

namespace CloudinaryFramework.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImagesController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] IFormFile file)
        {
            var apiKey = Request.Headers["X-Tenant"].ToString();

            if (file == null || string.IsNullOrEmpty(apiKey))
            {
                return BadRequest("Invalid file or API Key.");
            }

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                var imageBytes = stream.ToArray();
                var image = await _imageService.UploadImage(apiKey, imageBytes);
                return Ok(image);
            }
        }
    }
}
