using Microsoft.AspNetCore.Identity;

namespace PhoneStore.Models
{
    public class User : IdentityUser<int>
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}