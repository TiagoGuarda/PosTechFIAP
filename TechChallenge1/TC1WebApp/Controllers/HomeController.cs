using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TC1WebApp.Interfaces;
using TC1WebApp.Models;

namespace TC1WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAPIService _apiService;
        private readonly IFileUploadService _fileUploadService;

        public HomeController(ILogger<HomeController> logger, IAPIService apiService, IFileUploadService fileUploadService)
        {
            _logger = logger;
            _apiService = apiService;
            _fileUploadService = fileUploadService;
        }

        public IActionResult Index()
        {
            IEnumerable<FileRecordViewModel> arquivos = _apiService.GetAllFiles();

            return View(arquivos);
        }

        [HttpPost]
        public IActionResult Index(IFormFile arquivo)
        {
            if (ModelState.IsValid)
            {
                if (_fileUploadService.UploadFile(arquivo))
                    _apiService.AddFileRecord(arquivo.FileName, string.Format("{0}/{1}", _fileUploadService.GetBlobUrl(), arquivo.FileName));
            }

            return Index();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}