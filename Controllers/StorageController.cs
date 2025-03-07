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
        [Route("UploadBlobImage")]
        public async Task<IActionResult> UploadBlobs(IFormFile file)
        {
            var response = await _service.UploadFile(file);
            return Ok(response);
        }

        [HttpGet]
        [Route("GetAllBlobs")]
        public async Task<IActionResult> GetAllBlobs()
        {
            var response = await _service.GetUploadedBlobs();
            return Ok(response);
        }
    }
}
