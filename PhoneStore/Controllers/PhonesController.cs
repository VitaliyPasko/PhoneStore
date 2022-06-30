using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult Index(int? brandId, string name)
        {
            
            IQueryable<Phone> phones = _db.Phones
                .Include(p => p.Brand)
                .AsQueryable();
            IEnumerable<Brand> brands = _db.Brands;
            
            if (brandId is > 0)
                phones = _db.Phones.Where(p => p.BrandId == brandId);

            return View(new IndexViewModel
            {
                Brands = brands,
                Phones = phones.ToList()
            });
        }

        [HttpGet]
        public IActionResult Create()
        {
            PhoneCreateViewModel model = new PhoneCreateViewModel
            {
                Brands = _db.Brands.ToList()
            };
            return View(model);
        }
        
        [HttpPost]
        public IActionResult Create(PhoneCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                _db.Phones.Add(new Phone
                {
                    Name = model.Name,
                    Price = (decimal)model.Price!,
                    BrandId = model.BrandId
                });
                _db.SaveChanges();
            
                return RedirectToAction("Index");
            }

            model.Brands = _db.Brands.ToList();
            return View("Create", model);
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
            PhoneCreateViewModel model = new PhoneCreateViewModel
            {
                Id = phone.Id,
                Name = phone.Name,
                Price = phone.Price,
                BrandId = (int)phone.BrandId,
                Brands = _db.Brands.ToList()
            };
            return View(model);
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