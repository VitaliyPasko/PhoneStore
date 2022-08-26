using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneStore.Enums;
using PhoneStore.Helpers;
using PhoneStore.Models;
using PhoneStore.Services.Interfaces;
using PhoneStore.ViewModels.Account;

namespace PhoneStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly UserManager<User> _userManager;


        public AccountController(IAccountService accountService, UserManager<User> userManager)
        {
            _accountService = accountService;
            _userManager = userManager;
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
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _accountService.LogOf();
            return RedirectToAction("Index", "Phones");
        }

        [HttpGet]
        public IActionResult SearchAccounts(string searchTerm)
        {
            var users = _accountService.SearchUsersByAnyTerm(searchTerm);
            return PartialView("PartialViews/SearchResultPartial", users);
        }

        [HttpGet]
        public async Task<OkObjectResult> GetAuthUser()
        {
            User user = await _userManager.GetUserAsync(User);
            return Ok(user ?? new User());
        }
    }
}