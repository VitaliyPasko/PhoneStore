using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PhoneStore.Models;

namespace PhoneStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHostEnvironment _environment;

        public HomeController(
            ILogger<HomeController> logger, 
            IHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Test()
        {
            string filePath = Path.Combine("~/Files/", "test.txt");
            string fileType = "application/txt";
            return File(filePath, fileType);
        }
        
        [HttpGet]
        [ActionName("Test2")]
        public string Test(Phone phone)
        {
            return null;
        }
        
        [HttpGet]
        [ActionName("Test3")]
        public string Test(Phone[] phones)
        {
            return "tets";
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}