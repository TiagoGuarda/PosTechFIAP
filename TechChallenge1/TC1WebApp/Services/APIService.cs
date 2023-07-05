using System.Text.Json;
using System.Text.Json.Serialization;
using TC1WebApp.Interfaces;

namespace TC1WebApp.Services
{
    public class APIService : IAPIService
    {
        private readonly IConfiguration _configuration;

        public APIService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool AddFileRecord(string fileName, string filePath)
        {
            if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(filePath)) return false;

            var result = false;

            var baseAddress = _configuration.GetValue<string>("API");
            var path = "/API";

            var fileRecord = new FileRecordViewModel() { FileName = fileName, FilePath = filePath };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);

                var request = new HttpRequestMessage(HttpMethod.Post, path)
                {
                    Content = new StringContent(JsonSerializer.Serialize<FileRecordViewModel>(fileRecord), System.Text.Encoding.UTF8, "application/json"),
                };

                var response = client.SendAsync(request).Result;

                if (response.IsSuccessStatusCode)
                    result = true;
            }

            return result;
        }

        public IEnumerable<FileRecordViewModel> GetAllFiles()
        {
            IEnumerable<FileRecordViewModel> result = new List<FileRecordViewModel>();

            var baseAddress = _configuration.GetValue<string>("API");
            var path = "/API";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);

                var request = new HttpRequestMessage(HttpMethod.Get, path);

                var response = client.SendAsync(request).Result;

                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;

                    JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                    var fileRecords = JsonSerializer.Deserialize<IEnumerable<FileRecordViewModel>>(content, jsonSerializerOptions);

                    if (fileRecords != null)
                        result = fileRecords;
                }
            }

            return result;
        }
    }
}
