using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PhoneStore.ViewModels;

namespace PhoneStore.Controllers
{
    public class ErrorsController : Controller
    {
        private readonly Dictionary<int, ErrorViewModel> _errorResolver;

        public ErrorsController()
        {
            _errorResolver = new Dictionary<int, ErrorViewModel>();
            _errorResolver.Add(404, new ErrorViewModel
            {
                StatusCode = 404,
                Message = "Ресурс не найден",
                Title = "Oops... Страница не найдена"
            });
            _errorResolver.Add(400, new ErrorViewModel
            {
                StatusCode = 400,
                Message = "Сервер не смог обработать запрос",
                Title = "Oops... Ошибка"
            });
            _errorResolver.Add(500, new ErrorViewModel
            {
                StatusCode = 500,
                Message = "Сервер не смог обработать запрос",
                Title = "Oops... Ошибка"
            });
            _errorResolver.Add(777, new ErrorViewModel
            {
                StatusCode = 777,
                Message = "Сущность не найдена",
                Title = "Oops... Ошибка"
            });
        }

        [Route("Error/{statusCode}")]
        [ActionName("Error")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            if (_errorResolver.ContainsKey(statusCode))
            {
                return View(_errorResolver[statusCode]);
            }
            return View(_errorResolver[404]);
        }
    }
}