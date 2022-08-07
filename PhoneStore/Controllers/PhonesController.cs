using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneStore.Helpers;
using PhoneStore.Models;
using PhoneStore.Services.Abstractions;
using PhoneStore.ViewModels;

namespace PhoneStore.Controllers
{
    public class PhonesController : Controller
    {
        private readonly MobileContext _db;
        private readonly IPhoneService _phoneService;

        public PhonesController(
            MobileContext db,
            IPhoneService phoneService)
        {
            _db = db;
            _phoneService = phoneService;
        }
       
        [HttpGet]
        public IActionResult Index(int? brandId)
        {
            IEnumerable<Brand> brands = _db.Brands;
            IQueryable<Phone> phones = _db.Phones.AsQueryable();

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
                    await _phoneService.CreateAsync(model);

                    return RedirectToAction("Index");
                }

                model.Brands = _db.Brands.ToList();
                return View("Create", model);
            }
            //TODO: Добавить кастомный exception
            catch (FileNotFoundException)
            {
                return RedirectToAction("Error", "Errors", new {statusCode = 666});
            }
            catch(Exception)
            {
                return RedirectToAction("Error", "Errors", new {statusCode = 777});
            }
        }

        [HttpGet]
        public IActionResult Delete(int phoneId)
        {
            var phone = _db.Phones.FirstOrDefault(p => p.Id == phoneId);
            if (phone is null)
                return RedirectToAction("Error", "Errors", new {statusCode = 777});
            
            return View(phone);
        }
        
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult Confirm(int phoneId)
        {
            //TODO: Добавить удаление файла изображения.
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
                return RedirectToAction("Error", "Errors", new {statusCode = 777});
            
            PhoneCreateViewModel model = new PhoneCreateViewModel
            {
                Id = phone.Id,
                Name = phone.Name,
                Price = phone.Price,
                BrandId = (int)phone.BrandId,
                Image = phone.Image,
                Brands = brands
            };
            return View(model);
        }
        
        //TODO: Доделать
        [HttpPost]
        public IActionResult Edit(PhoneCreateViewModel model)
        {
            if (model is null)
                return RedirectToAction("Error", "Errors", new {statusCode = 777});
            string imagePath = string.Empty;
            if (model.File is null)
                imagePath = model.Image;
            
            _db.Phones.Update(model.MapToPhone(imagePath));
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult About(int? phoneId)
        {
            if (!phoneId.HasValue) return RedirectToAction("Error", "Errors", new {statusCode = 777});
            var phone = _db.Phones
                .Include(p => p.Brand)
                .Include(p => p.Feedbacks)
                    .ThenInclude(f => f.User)
                .FirstOrDefault(p => p.Id == phoneId);
            if (phone is null)
                return RedirectToAction("Error", "Errors", new {statusCode = 777});
            var phoneViewModel = new PhoneViewModel
            {
                Brand = phone.Brand,
                Feedbacks = phone.Feedbacks.Select(f => new FeedbackViewModel
                {
                    Id = f.Id,
                    Phone = f.Phone,
                    Text = f.Text,
                    User = f.User,
                    CreationDateTime = f.CreationDateTime,
                    UserId = f.UserId,
                    PhoneId = f.PhoneId
                })
                    .OrderByDescending(f => f.CreationDateTime)
                    .ToList(),
                Image = phone.Image,
                Name = phone.Name,
                Price = phone.Price,
                Id = phone.Id
            };
            return View(phoneViewModel);
        }
    }
}