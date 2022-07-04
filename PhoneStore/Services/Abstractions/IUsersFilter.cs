using System.Linq;
using PhoneStore.Models;

namespace PhoneStore.Services.Abstractions
{
    public interface IUsersFilter
    {
        IQueryable<User> FilterByName(IQueryable<User> users, string filterByName);
    }
}