using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhoneStore.Models;

namespace PhoneStore.Controllers
{
    public class PhonesController : Controller
    {
        private readonly MobileContext _db;

        public PhonesController(MobileContext db)
        {
            _db = db;
        }
        /// <summary>
        /// Действие, которое возвращает страницу со списком смартфонов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            List<Phone> phones = _db.Phones.ToList();
            
            return View(phones);
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
    }
}