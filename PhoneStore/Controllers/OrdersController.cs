using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneStore.Models;

namespace PhoneStore.Controllers
{
    public class OrdersController : Controller
    {
        private readonly MobileContext _db;

        public OrdersController(MobileContext db)
        {
            _db = db;
        }

        // GET
        public IActionResult Index()
        {
            var orders = _db.Orders
                .Include(p => p.Phone)
                .ToList();
            
            return View(orders);
        }
        
        [HttpGet]
        public IActionResult Create(int phoneId)
        {
            var phone = _db.Phones.FirstOrDefault(p => p.Id == phoneId);
            if (phone is null)
                return RedirectToAction("Error", "Errors", new {statusCode = 777});
            Order order = new Order
            {
                Phone = phone
            };
            
            return View(order);
        }
        
        [HttpPost]
        public IActionResult Create(Order order)
        {
            _db.Orders.Add(order);
            _db.SaveChanges();
            
            return RedirectToAction("Index");
        }
    }
}