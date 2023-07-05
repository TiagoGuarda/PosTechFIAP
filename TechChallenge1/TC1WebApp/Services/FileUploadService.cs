using Azure.Storage.Blobs;
using TC1WebApp.Interfaces;

namespace TC1WebApp.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly string _blobConnectionString;
        private readonly string _blobContainerName;
        private readonly string _blobUrl;

        public FileUploadService(IConfiguration configuration)
        {
            _blobConnectionString = configuration.GetValue<string>("StorageConnectionString");
            _blobContainerName = configuration.GetValue<string>("StorageContainer");
            _blobUrl = configuration.GetValue<string>("StorageUrl");
        }

        public bool UploadFile(IFormFile file)
        {
            if (file == null) return false;

            var result = false;

            var blobContainer = new BlobContainerClient(_blobConnectionString, _blobContainerName);
            var blobClient = blobContainer.GetBlobClient(file.FileName);

            try
            {
                blobClient.Upload(file.OpenReadStream());
                result = true;
            }
            catch { }

            return result;
        }

        public string GetBlobUrl() { return _blobUrl; }
    }
}
