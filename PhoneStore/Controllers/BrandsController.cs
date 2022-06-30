using Microsoft.AspNetCore.Mvc;
using PhoneStore.Models;
using PhoneStore.ViewModels;

namespace PhoneStore.Controllers
{
    public class BrandsController : Controller
    {
        private readonly MobileContext _db;

        public BrandsController(MobileContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(CreateBrandViewModel model)
        {
            _db.Brands.Add(new Brand
            {
                Name = model.Name
            });
            _db.SaveChanges();
            
            return RedirectToAction("Index", "Phones");
        }
    }
}