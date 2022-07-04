using System.Linq;
using System.Threading.Tasks;
using PhoneStore.Models;
using PhoneStore.Services.Abstractions;
using Order = PhoneStore.Enums.Order;

namespace PhoneStore.Services
{
    public class UsersSortService : IUsersSortService
    {
        public IQueryable<User> Sort(IQueryable<User> users, Order order)
        {
            switch (order)
            {
                case Order.AgeDesc:
                    return users.OrderByDescending(u => u.Age);
                case Order.AgeAsc:
                    return users.OrderBy(u => u.Age);
                case Order.NameDesc:
                    return users.OrderByDescending(u => u.Name);
                default: return users.OrderBy(u => u.Name);
            }
        }
    }
}