using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using PhoneStore.Models;
using PhoneStore.Services;
using PhoneStore.ViewModels;

namespace PhoneStore.Controllers
{
    public class PhonesController : Controller
    {
        private readonly MobileContext _db;
        private readonly IHostEnvironment _environment;
        private readonly UploadService _uploadService;

        public PhonesController(
            MobileContext db, 
            IHostEnvironment environment, UploadService uploadService)
        {
            _db = db;
            _environment = environment;
            _uploadService = uploadService;
        }
        /// <summary>
        /// Действие, которое возвращает страницу со списком смартфонов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index(int? id, string? name)
        {
            var phones = _db.Phones.AsQueryable();
            if (id.HasValue)
            {
                phones = _db.Phones.Where(p => p.Id == id);
            }
            if (!string.IsNullOrEmpty(name))
            {
                phones = phones.Where(p => string.Equals(p.Name, name, StringComparison.CurrentCultureIgnoreCase));
            }

            var phoneList = phones.ToList();
            return View(phones.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(Phone phone)
        {
            _db.Phones.Add(phone);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int phoneId)
        {
            var phone = _db.Phones.FirstOrDefault(p => p.Id == phoneId);
            if (phone is null)
                return BadRequest();
            
            return View(phone);
        }
        
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult Confirm(int phoneId)
        {
            var phone = _db.Phones.FirstOrDefault(p => p.Id == phoneId);
            if (phone is null)
                return BadRequest();
            _db.Phones.Remove(phone);
            _db.SaveChanges();
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int phoneId)
        {
            var phone = _db.Phones.FirstOrDefault(p => p.Id == phoneId);
            if (phone is null)
            {
                return BadRequest();
            }
            return View(phone);
        }
        
        [HttpPost]
        public IActionResult Edit(Phone phone)
        {
            if (phone is null)
            {
                return BadRequest();
            }

            _db.Phones.Update(phone);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        
        [HttpGet]
        public IActionResult UploadFile()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> UploadFile(FileViewModel model)
        {
            string dirPath = Path.Combine(_environment.ContentRootPath, "wwwroot/Files");
            string fileName = $"{model.File.FileName}";
            await _uploadService.UploadAsync(dirPath, fileName, model.File);

            return Ok("Файл успешно загружен");
        }
    }
}