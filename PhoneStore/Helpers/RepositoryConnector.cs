using Microsoft.Extensions.DependencyInjection;
using PhoneStore.Repositories;
using PhoneStore.Repositories.Interfaces;

namespace PhoneStore.Helpers
{
    public static class RepositoryConnector
    {
        public static IServiceCollection AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPhoneRepository, PhoneRepository>();
            return services;
        }
    }
}