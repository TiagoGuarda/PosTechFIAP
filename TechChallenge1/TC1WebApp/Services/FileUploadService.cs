using Azure.Storage.Blobs;
using TC1WebApp.Interfaces;

namespace TC1WebApp.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly string _blobConnectionString;
        private readonly string _blobContainerName;

        public FileUploadService(IConfiguration configuration)
        {
            _blobConnectionString = configuration.GetValue<string>("Storage:ConnectionString");
            _blobContainerName = configuration.GetValue<string>("Storage:Container");
        }

        public bool UploadFile(IFormFile file)
        {
            if (file == null) return false;

            var result = false;

            var blobContainer = new BlobContainerClient(_blobConnectionString, _blobContainerName);
            var blobClient = blobContainer.GetBlobClient(file.FileName);

            blobClient.Upload(file.OpenReadStream());

            return result;
        }
    }
}
