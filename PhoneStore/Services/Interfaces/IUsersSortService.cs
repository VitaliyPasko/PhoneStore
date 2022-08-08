using System.Linq;
using PhoneStore.Models;
using Order = PhoneStore.Enums.Order;

namespace PhoneStore.Services.Interfaces
{
    public interface IUsersSortService
    {
        IQueryable<User> Sort(IQueryable<User> users, Order order);
    }
}