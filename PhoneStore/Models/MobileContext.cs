using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PhoneStore.Models
{
    public class MobileContext : IdentityDbContext<User, Role, int>
    {
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Brand> Brands { get; set; }

        public MobileContext(DbContextOptions<MobileContext> options) : base(options)
        {
        }
    }
}