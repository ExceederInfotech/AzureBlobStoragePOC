using AzurBlobStorageAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzurBlobStorageAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly AzureBlobService _service;
        public StorageController(AzureBlobService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("UploadBlogImage")]
        public async Task<IActionResult> UploadBlogImage(IFormFile file)
        {
            var response = await _service.UploadBlogImage(file);
            return Ok(response);
        }

        [HttpGet]
        [Route("GetUploadedBlogImages")]
        public async Task<IActionResult> GetUploadedBlogImages()
        {
            var response = await _service.GetUploadedBlogImages();
            return Ok(response);
        }
    }
}