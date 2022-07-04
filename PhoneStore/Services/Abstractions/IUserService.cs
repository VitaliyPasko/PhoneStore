using System.Threading.Tasks;
using PhoneStore.Enums;
using PhoneStore.ViewModels;

namespace PhoneStore.Services.Abstractions
{
    public interface IUserService
    {
        Task<UsersViewModel> GetAll(Order order, string filterByName, int pageSize, int page);
    }
}