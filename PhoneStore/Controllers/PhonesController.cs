using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneStore.Helpers;
using PhoneStore.Mappers;
using PhoneStore.Models;
using PhoneStore.Services.Interfaces;
using PhoneStore.ViewModels;
using PhoneStore.ViewModels.Account;
using PhoneStore.ViewModels.PhoneViewModels;

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
            IPhoneEditable model = new PhoneCreateViewModel
            {
                Brands = _db.Brands.ToList()
            };
            return View(model);
        }
        
        // [HttpPost]
        // public async Task<IActionResult> Create(PhoneCreateViewModel model)
        // {
        //     try
        //     {
        //         if (ModelState.IsValid)
        //         {
        //             await _phoneService.CreateAsync(model);
        //
        //             return RedirectToAction("Index");
        //         }
        //
        //         model.Brands = _db.Brands.ToList();
        //         return View("Create", model);
        //     }
        //     //TODO: Добавить кастомный exception
        //     catch (FileNotFoundException)
        //     {
        //         return RedirectToAction("Error", "Errors", new {statusCode = 666});
        //     }
        //     catch(Exception)
        //     {
        //         return RedirectToAction("Error", "Errors", new {statusCode = 777});
        //     }
        // }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]PhoneCreateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _phoneService.CreateAsync(model);
                    return Ok();
                }

                model.Brands = _db.Brands.ToList();
                return ValidationProblem();
            }
            catch (FileNotFoundException)
            {
                return BadRequest();
            }
            catch(Exception)
            {
                return BadRequest();
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
                return RedirectToAction("Error", "Errors", new {statusCode = 777});
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
            
            IPhoneEditable model = new PhoneEditViewModel
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

        // [HttpGet]
        // public IActionResult About(int? phoneId)
        // {
        //     try
        //     {
        //         if (!phoneId.HasValue) return RedirectToAction("Error", "Errors", new {statusCode = 777});
        //         var phoneViewModel = _phoneService.GetById(phoneId.Value);
        //         return View(phoneViewModel);
        //     }
        //     catch (NullReferenceException)
        //     {
        //         return RedirectToAction("Error", "Errors", new {statusCode = 777});
        //     }
        // }
        [HttpGet]
        public IActionResult About(int? phoneId)
        {
            try
            {
                if (!phoneId.HasValue) return ValidationProblem();
                var phoneViewModel = _phoneService.GetById(phoneId.Value);
                return Ok(phoneViewModel);
            }
            catch (NullReferenceException)
            {
                return BadRequest();
            }
        }
        
    }
}