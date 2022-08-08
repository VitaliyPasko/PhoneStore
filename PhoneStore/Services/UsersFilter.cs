using System.Linq;
using PhoneStore.Models;
using PhoneStore.Services.Interfaces;

namespace PhoneStore.Services
{
    public class UsersFilter : IUsersFilter
    {
        public IQueryable<User> FilterByName(IQueryable<User> users, string filterByName)
        {
            return string.IsNullOrEmpty(filterByName) 
                ? users 
                : users.Where(u => u.Name.Contains(filterByName));
        }
    }
}