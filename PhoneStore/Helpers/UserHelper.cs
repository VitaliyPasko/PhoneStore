using System.Linq;
using PhoneStore.Models;
using PhoneStore.ViewModels;
using PhoneStore.ViewModels.Account;

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
        
        public static UserViewModel MapToUserViewModel(
            this User self)
        {
            return new UserViewModel
            {
                Age = self.Age,
                Email = self.Email,
                Id = self.Id,
                Name = self.Name,
                Username = self.UserName
            };
        }
    }
}