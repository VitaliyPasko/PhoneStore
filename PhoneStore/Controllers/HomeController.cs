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

        // [HttpGet]
        // public IActionResult Test()
        // {
        //     string filePath = Path.Combine(_environment.ContentRootPath, "Files/test.pdf");
        //     string fileType = "application/pdf";
        //     string fileName = "test.pdf";
        //     return PhysicalFile(filePath, fileType, fileName);
        // }
        
        // [HttpGet]
        // public IActionResult Test()
        // {
        //     string filePath = Path.Combine(_environment.ContentRootPath, "Files/test.pdf");
        //     byte[] bytes = System.IO.File.ReadAllBytes(filePath);
        //     string fileType = "application/pdf";
        //     string fileName = "test.pdf";
        //     return File(bytes, fileType, fileName);
        // }
        
        // [HttpGet]
        // public IActionResult Test()
        // {
        //     string filePath = Path.Combine(_environment.ContentRootPath, "Files/test.pdf");
        //     byte[] bytes = System.IO.File.ReadAllBytes(filePath);
        //     string fileType = "application/pdf";
        //     string fileName = "test.pdf";
        //     return File(bytes, fileType, fileName);
        // }
        
        // [HttpGet]
        // public IActionResult Test()
        // {
        //     string filePath = Path.Combine(_environment.ContentRootPath, "Files/test.pdf");
        //     FileStream stream = new FileStream(filePath, FileMode.Open);
        //     string fileType = "application/pdf";
        //     string fileName = "test.pdf";
        //     return File(stream, fileType, fileName);
        // }
        
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
            return "tets";
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
    }
}