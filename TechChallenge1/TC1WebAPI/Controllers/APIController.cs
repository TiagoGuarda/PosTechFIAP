using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Data.SqlClient;

namespace TC1WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class APIController : ControllerBase
    {
        private readonly ILogger<APIController> _logger;
        private readonly IConfiguration _configuration;

        public APIController(ILogger<APIController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet(Name = "GetAllFiles")]
        public async Task<IEnumerable<FileRecord>> GetAllFiles()
        {
            using (var db = new SqlConnection(_configuration.GetValue<string>("DB")))
            {
                var sql = "SELECT * FROM FileRecords";
                return await db.QueryAsync<FileRecord>(sql);
            }
        }

        [HttpPost(Name = "AddFileRecord")]
        public async void AddFileRecord(FileRecord fileRecord)
        {
            if (fileRecord == null) return;

            using (var db = new SqlConnection(_configuration.GetValue<string>("DB")))
            {
                var sql = "INSERT INTO FileRecords (FileName, FilePath) VALUES (@FileName, @FilePath)";
                await db.ExecuteAsync(sql, fileRecord);
            }
        }

    }
}