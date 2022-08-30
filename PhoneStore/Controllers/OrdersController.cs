
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneStore.Mappers;
using PhoneStore.Models;
using PhoneStore.Services.Interfaces;
using PhoneStore.ViewModels;

namespace PhoneStore.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IPhoneService _phoneService;
        private readonly UserManager<User> _userManager;
        public OrdersController(IOrderService orderService, IPhoneService phoneService, UserManager<User> userManager)
        {
            _orderService = orderService;
            _phoneService = phoneService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var orders = _orderService.GetAll();
            return View(orders);
        }
        
        [HttpGet]
        public async Task<IActionResult> Create(int phoneId)
        {
            User user = await _userManager.GetUserAsync(User);
            var phone = _phoneService.GetById(phoneId);
            if (phone is null)
                return RedirectToAction("Error", "Errors", new {statusCode = 777});
            OrderViewModel order = new OrderViewModel
            {
                Phone = phone,
                User = user.MapToUserViewModel()
            };
            
            return View(order);
        }
        
        [HttpPost]
        public IActionResult Create(OrderViewModel order)
        {
            order.UserId = int.Parse(_userManager.GetUserId(User));
            order.User = null;
            _orderService.Create(order.MapToOrderViewModel());
            return RedirectToAction("Index");
        }

        public IActionResult GetAllByUserId(int id)
        {
            var model = _orderService.GetByUserId(id);
            return Json(model);
        }
    }
}