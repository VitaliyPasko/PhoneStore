using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using PhoneStore.DataObjects;
using PhoneStore.Models;
using PhoneStore.ViewModels.Account;

namespace PhoneStore.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IdentityResult> Register(RegisterViewModel model);
        Task<IdentityResult> LogIn(LoginViewModel model);
        Task LogOf();
        IEnumerable<User> SearchUsersByAnyTerm(string searchTerm);
    }
}