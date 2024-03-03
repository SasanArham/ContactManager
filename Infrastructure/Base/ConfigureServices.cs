using Application.Base;
using Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Ardalis.GuardClauses;
using Application.Common;
using Infrastructure.Common;

namespace Infrastructure.Base
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            Guard.Against.Null(connectionString, "connectionString", "Connection string 'DefaultConnection' not found.");
            services.AddDbContext<DatabaseContext>(
                (sp, options) =>
                {
                    options.UseLazyLoadingProxies();
                    options.UseSqlServer(connectionString, b => b.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName));

                });
            services.AddScoped<IDatabaseContext>(provider => provider.GetRequiredService<DatabaseContext>());

            services.AddStackExchangeRedisCache(redisOptions =>
            {
                var connection = configuration.GetConnectionString("Redis");
                Guard.Against.Null(connection, "connectionString", "Connection string 'Redis' not found.");
                redisOptions.Configuration = connection;
            });


            services.AddScoped<IDistributedCachProvider, DistributedCachProvider>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

    }
}
