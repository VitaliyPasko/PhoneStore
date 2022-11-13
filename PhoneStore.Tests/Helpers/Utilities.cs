using System.Linq;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PhoneStore.Models;
using PhoneStore.ViewModels.PhoneViewModels;

namespace PhoneStore.Tests.Helpers
{
    public static class Utilities
    {
        private const string ConnectionString =
            "Server=localhost;Port=5432;database=testDb;Username=postgres;Password=postgres;";
        public static WebApplicationFactory<Startup> SubstituteDbOnTestDb()
        {
            return new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var dbContextDescriptor =
                        services.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<MobileContext>));
                    services.Remove(dbContextDescriptor);
                    services.AddDbContext<MobileContext>(options =>
                    {
                        options.UseNpgsql(ConnectionString);
                    });
                });
            });
        }

        public static int CreateBrandAndReturnBrandId(MobileContext context)
        {
            Brand brand = new Brand {Name = "Asus"};
            context.Brands.Add(brand);
            context.SaveChanges();
            return brand.Id;
        }
        
        public static int CreatePhoneAndReturnPhoneId(MobileContext context)
        {
            Phone phone = new Phone
            {
                Name = "Test",
                Price = 3000M,
                Brand = new Brand{Name = "Adidas"}
            };
            context.Phones.Add(phone);
            context.SaveChanges();
            return phone.Id;
        }
    }
}