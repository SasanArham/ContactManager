using Domain.Modules.ContactManagement.People.Services;
using Microsoft.Extensions.DependencyInjection;
using Repository.Modules.ContactManagement.People;

namespace Repository.Base
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPersonRepository, PersonRepository>();

            return services;
        }
    }
}
