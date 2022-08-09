using Microsoft.Extensions.DependencyInjection;
using PhoneStore.Validations;

namespace PhoneStore.Helpers
{
    public static class ValidationsConnector
    {
        public static void AddValidationServices(this IServiceCollection services)
        {
            services.AddTransient<FeedbackValidation>();
        }
    }
}