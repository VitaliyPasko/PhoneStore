using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PhoneStore.Models;

namespace PhoneStore.Services
{
    public class AdminInitializerService
    {
        public static async Task SeedAdminUser(
            RoleManager<Role> roleManager,
            UserManager<User> userManager)
        {
            string adminEmail = "admin@admin.com";
            string adminPassword = "Admin1234@";
  
            var roles = new [] { "admin", "user" };
 
            foreach (var role in roles)
            {
                if (await roleManager.FindByNameAsync(role) is null)
                    await roleManager.CreateAsync(new Role(role));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User { Email = adminEmail, UserName = adminEmail };
                IdentityResult result = await userManager.CreateAsync(admin, adminPassword);
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(admin, "admin");
            }
        }
    }
}