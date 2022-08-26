using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhoneStore.Models;
using PhoneStore.Services;
using PhoneStore.Services.Interfaces;

namespace PhoneStore.Helpers
{
    public static class ServiceConnector
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IBasketService, BasketService>();
            services.AddTransient<UploadService>();
            
            services.AddTransient<IDefaultPhoneImagePathProvider>(_ =>
                new DefaultPhoneImagePathProvider(configuration["PathToDefaultAvatar:Path"]));
            
            services.AddTransient<IPhoneService, PhoneService>();
            // services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUsersSortService, UsersSortService>();
            services.AddTransient<IUsersFilter, UsersFilter>();
            services.AddTransient<IPaginationService<User>, PaginationService<User>>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IUsersSearcher, UsersSearcher>();
            services.AddTransient<IFeedbackService, FeedbackService>();
            services.AddTransient<IPhoneService, PhoneService>();
            services.AddTransient<IOrderService, OrderService>();
            return services;
        }
    }
}