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

        public HomeController(ILogger<HomeController> logger, IAPIService apiService)
        {
            _logger = logger;
            _apiService = apiService;
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
                //TODO: Insert file into azure storage
                var storage = "";

                _apiService.AddFileRecord(arquivo.FileName, string.Format("{0}/{1}", storage, arquivo.FileName));
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