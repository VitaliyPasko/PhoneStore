using System.Linq;
using PhoneStore.Models;
using PhoneStore.ViewModels;

namespace PhoneStore.Helpers
{
    public static class UserHelper
    {
        public static UsersViewModel MapToUsersViewModel(
            this IQueryable<User> self, 
            string filter, 
            int page, 
            int count,
            int pageSize)
        {
            return new UsersViewModel
            {
                Users = self.ToArray(),
                Filter = filter,
                PageViewModel = new PageViewModel(page, count, pageSize)
            };
        }
    }
}