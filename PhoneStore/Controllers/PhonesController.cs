using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using PhoneStore.Models;
using PhoneStore.Services;
using PhoneStore.Services.Abstractions;
using PhoneStore.ViewModels;

namespace PhoneStore.Controllers
{
    public class PhonesController : Controller
    {
        private readonly MobileContext _db;
        private readonly IHostEnvironment _environment;
        private readonly UploadService _uploadService;
        private readonly IDefaultPhoneImagePathProvider _imagePathProvider;

        public PhonesController(
            MobileContext db, 
            IHostEnvironment environment, 
            UploadService uploadService, 
            IDefaultPhoneImagePathProvider imagePathProvider)
        {
            _db = db;
            _environment = environment;
            _uploadService = uploadService;
            _imagePathProvider = imagePathProvider;
        }
       
        [HttpGet]
        public IActionResult Index(int? brandId, string name)
        {
            IEnumerable<Brand> brands = _db.Brands;
            IQueryable<Phone> phones = _db.Phones
                .AsQueryable();

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
        public async Task<IActionResult> Create(PhoneCreateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string imagePath;
                    if (model.File is null)
                        imagePath = _imagePathProvider.GetPathToDefaultImage();
                    else
                    {
                        var brand = _db.Brands.FirstOrDefault(b => b.Id == model.BrandId);
                        string dirPath = Path.Combine(_environment.ContentRootPath, $"wwwroot\\images\\phoneImages\\{brand!.Name}");
                        string fileName = $"{model.File.FileName}";
                        await _uploadService.UploadAsync(dirPath, fileName, model.File);
                        imagePath = $"images\\phoneImages\\{brand!.Name}\\{fileName}";
                    }
                
                    _db.Phones.Add(new Phone
                    {
                        Image = imagePath,
                        Name = model.Name,
                        Price = (decimal)model.Price!,
                        BrandId = model.BrandId
                    });
                    await _db.SaveChangesAsync();
            
                    return RedirectToAction("Index");
                }
            }
            //TODO: Добавить кастомный exception
            catch (FileNotFoundException e)
            {
                return RedirectToAction("Error", "Errors", new {statusCode = 666});
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
            var brands = _db.Brands.ToList();
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
                Brands = brands
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

        [HttpGet]
        public IActionResult About(int? id)
        {
            if (!id.HasValue) return RedirectToAction("Error", "Errors", new {statusCode = 777});
            var phone = _db.Phones
                .Include(p => p.Brand)
                .FirstOrDefault(p => p.Id == id);
            if (phone is null)
                return RedirectToAction("Error", "Errors", new {statusCode = 777});
            
            return View(phone);
        }
    }
}