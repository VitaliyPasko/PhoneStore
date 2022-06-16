using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneStore.Models;

namespace PhoneStore.Controllers
{
    public class PhoneController : Controller
    {
        private readonly MobileContext _db;

        public PhoneController(MobileContext db)
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
    }
}