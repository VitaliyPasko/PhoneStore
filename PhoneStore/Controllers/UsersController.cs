using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PhoneStore.Models;
using PhoneStore.ViewModels;
using Order = PhoneStore.Enums.Order;

namespace PhoneStore.Controllers
{
    public class UsersController : Controller
    {
        private readonly MobileContext _db;

        public UsersController(MobileContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index(
            string filteringCriterionByName, 
            int page = 1, 
            Order order = Order.NameAsc)
        {
            int pageSize = 2;
            IQueryable<User> users  = _db.Users.AsQueryable();
            if (!string.IsNullOrEmpty(filteringCriterionByName))
                users = users.Where(u => u.Name.Contains(filteringCriterionByName));
            int count = users.Count();
            
            users = _db
                .Users
                .OrderBy(u => u.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
            ViewBag.NameSort = order == Order.NameAsc ? Order.NameDesc : Order.NameAsc;
            ViewBag.AgeSort = order == Order.AgeAsc ? Order.AgeDesc : Order.AgeAsc;
            
            
            
            switch (order)
            {
                case Order.AgeDesc:
                    users = users.OrderByDescending(u => u.Age);
                    break;
                case Order.AgeAsc:
                    users = users.OrderBy(u => u.Age);
                    break;
                case Order.NameDesc:
                    users = users.OrderByDescending(u => u.Name);
                    break;
                default:
                    users = users.OrderBy(u => u.Name);
                    break;
            }

            UsersViewModel model = new UsersViewModel
            {
                Filter = filteringCriterionByName,
                Users = users.ToArray(),
                PageViewModel = new PageViewModel(page, count, pageSize )
            };
            
            return View(model);
        }
    }
}