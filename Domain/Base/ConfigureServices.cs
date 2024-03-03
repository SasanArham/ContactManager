using Domain.Modules.ContactManagement.People.Builders;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Base
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IPersonBuilder, PersonBuilder>();

            return services;
        }
    }
}
