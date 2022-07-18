using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneStore.Services.Abstractions;
using Order = PhoneStore.Enums.Order;

namespace PhoneStore.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string filterByName, Order order = Order.AgeAsc, int page = 1)
        {
            ViewBag.NameSort = order == Order.NameAsc ? Order.NameDesc : Order.NameAsc;
            ViewBag.AgeSort = order == Order.AgeAsc ? Order.AgeDesc : Order.AgeAsc;
            var model = await _userService.GetAll(order, filterByName, page);
            
            return View(model);
        }
    }
}