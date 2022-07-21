using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PhoneStore.Enums;
using PhoneStore.Models;
using PhoneStore.Services.Abstractions;
using PhoneStore.ViewModels.Account;
using IdentityResult = PhoneStore.DataObjects.IdentityResult;

namespace PhoneStore.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountService(
            UserManager<User> userManager, 
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> Register(RegisterViewModel model)
        {
            if (model is null) return new IdentityResult
            {
                StatusCodes = StatusCodes.Error, 
                ErrorMessages = new List<string>{"Внутренняя ошибка"}
            };
            
            User user = new User
            {
                Email = model.Email,
                UserName = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "user");
                await _signInManager.SignInAsync(user, false);
                return new IdentityResult{StatusCodes = StatusCodes.Success};
            }
            
            var errors = result.Errors.Select(error => error.Description).ToList();

            return new IdentityResult
            {
                ErrorMessages = errors, 
                StatusCodes = StatusCodes.Error
            };
        }

        public async Task<IdentityResult> LogIn(LoginViewModel model)
        {
            User user = await _userManager.FindByEmailAsync(model.Email);
            SignInResult result = await _signInManager.PasswordSignInAsync(
                user,
                model.Password,
                model.RememberMe,
                false
            );
            if (result.Succeeded)
                return new IdentityResult {StatusCodes = StatusCodes.Success};
            
            return new IdentityResult
            {
                StatusCodes = StatusCodes.Error,
                ErrorMessages = new List<string>{"Неправильный логин и (или) пароль"}
            };
        }

        public Task<IdentityResult> LogOf()
        {
            throw new System.NotImplementedException();
        }
    }
}