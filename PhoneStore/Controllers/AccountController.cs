using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneStore.Enums;
using PhoneStore.Helpers;
using PhoneStore.Models;
using PhoneStore.Services.Abstractions;
using PhoneStore.ViewModels.Account;

namespace PhoneStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) 
                return View(model);
            
            var result = await _accountService.Register(model);
            if (result.StatusCodes == StatusCodes.Success) 
                return RedirectToAction("Index", "Phones");
            
            if (result.ErrorMessages.Any())
                ModelState.AddErrors(result.ErrorMessages);
            
            return View(model);
        }
        
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _accountService.LogIn(model);
            if (result.StatusCodes == StatusCodes.Success)
            {
                if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    return Redirect(model.ReturnUrl);
                return RedirectToAction("Index", "Phones");
            }
            if (result.ErrorMessages.Any())
                ModelState.AddErrors(result.ErrorMessages);
            
            return View(model);
        }
    }
}