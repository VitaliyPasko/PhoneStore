using System.Collections.Generic;
using PhoneStore.Models;

namespace PhoneStore.ViewModels
{
    public class UsersViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public string Filter { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}