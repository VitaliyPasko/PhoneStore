using Microsoft.AspNetCore.Mvc;
using PhoneStore.Enums;
using PhoneStore.Services.Abstractions;
using PhoneStore.ViewModels;

namespace PhoneStore.Controllers
{
    public class BasketsController : Controller
    {
        private readonly IBasketService _basketService;

        public BasketsController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet]
        public IActionResult Add(int id)
        {
            var result = _basketService.AddPhone(id);
            ResponseMessageViewModel response = result switch
            {
                StatusCodes.Success => new ResponseMessageViewModel {Message = "Продукт успешно добавлен"},
                StatusCodes.EntityNotFound => new ResponseMessageViewModel {Message = "Продукт не найден"},
                _ => new ResponseMessageViewModel {Message = "Продукт не найден"}
            };

            response.Status = result;
            
            return View(response);
        }

        public IActionResult Index()
        {
            var baskets = _basketService.GetAll();
            return View(baskets);
        }

        [HttpGet]
        public IActionResult Remove(int id)
        {
            _basketService.Remove(id);

            return RedirectToAction("Index");
        }
    }
}