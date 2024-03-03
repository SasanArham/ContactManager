using Ardalis.GuardClauses;
using Domain.Modules.ContactManagement.People.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.Modules.ContactManagement.People;

namespace Repository.Base
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.Decorate<IPersonRepository, CachedPersonRepository>();
            services.AddStackExchangeRedisCache(redisOptions =>
            {
                var connection = configuration.GetConnectionString("Redis");
                Guard.Against.Null(connection, "connectionString", "Connection string 'Redis' not found.");
                redisOptions.Configuration = connection;
            });

            return services;
        }
    }
}
