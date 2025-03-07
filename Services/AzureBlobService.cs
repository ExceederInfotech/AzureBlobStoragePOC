using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace AzurBlobStorageAPI.Services
{
    public class AzureBlobService
    {
        readonly BlobServiceClient _blobClient;
        readonly BlobContainerClient _containerClient;
        private readonly string? azureConnectionString = null;
        private readonly string containerName;
        public AzureBlobService(IConfiguration configuration)
        {
            azureConnectionString = configuration.GetConnectionString("AzureStorageConnectionString");
            _blobClient = new BlobServiceClient(azureConnectionString);
            containerName = "blogimages";
            _containerClient = _blobClient.GetBlobContainerClient(containerName);
        }
        public async Task<List<Azure.Response<BlobContentInfo>>> UploadBlogImage(IFormFile file)
        {
            var azureResponse = new List<Azure.Response<BlobContentInfo>>();
            string fileName = file.FileName;
            using (var memoryStream = new MemoryStream())
            {
                BlobContainerClient container = _blobClient.GetBlobContainerClient(containerName);
                await container.CreateIfNotExistsAsync();
                file.CopyTo(memoryStream);
                memoryStream.Position = 0;
                var client = await _containerClient.UploadBlobAsync(fileName, memoryStream, default);
                azureResponse.Add(client);
            }
            return azureResponse;
        }
        public async Task<List<BlobDto>> GetUploadedBlogImages()
        {
            BlobContainerClient container = new(azureConnectionString, containerName);
            List<BlobDto> files = [];
            await foreach (BlobItem file in container.GetBlobsAsync())
            {
                string uri = container.Uri.ToString();
                var name = file.Name;
                var fullUri = $"{uri}/{name}";

                files.Add(new BlobDto
                {
                    Uri = fullUri,
                    Name = name,
                    ContentType = file.Properties.ContentType
                });
            }
            return files;
        }
    }
}
