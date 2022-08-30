using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace PhoneStore.Models
{
    public class User : IdentityUser<int>
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public IEnumerable<Feedback> Feedbacks { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}