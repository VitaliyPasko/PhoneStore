using System.Threading.Tasks;
using PhoneStore.DataObjects;
using PhoneStore.ViewModels.Account;

namespace PhoneStore.Services.Abstractions
{
    public interface IAccountService
    {
        Task<IdentityResult> Register(RegisterViewModel model);
        Task<IdentityResult> LogIn(LoginViewModel model);
        Task LogOf();
    }
}