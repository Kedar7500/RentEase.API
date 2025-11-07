using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentEase.API.Models.DTOs;
using RentEase.API.Services.Interfaces;

namespace RentEase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageService imageService;

        public ImagesController(IImageService imageService)
        {
            this.imageService = imageService;
        }

        // POST : /api/images/upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {
            ValidateFileUpload(request);

            if (ModelState.IsValid)
            {
                // use service to upload image
                await imageService.UploadImage(request);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "UnSupported File Extension");
            }

            if(request.File.Length > 10485760)
            {
                ModelState.AddModelError("file","File size more than 10MB, please upload a smaller size file");
            }
        }
    }
}
