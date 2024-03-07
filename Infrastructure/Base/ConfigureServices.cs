using Application.Base;
using Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Ardalis.GuardClauses;
using Application.Common;
using Infrastructure.Common;
using StackExchange.Redis;

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

            var redisConnection = configuration.GetConnectionString("Redis");
            Guard.Against.Null(redisConnection, "connectionString", "Connection string 'Redis' not found.");
            services.AddStackExchangeRedisCache(redisOptions =>
            {
                redisOptions.Configuration = redisConnection;
            });
            services.AddSingleton<IConnectionMultiplexer>(sp =>
                 ConnectionMultiplexer.Connect(new ConfigurationOptions
                 {
                     EndPoints = { redisConnection },
                     //Ssl = true,
                     AbortOnConnectFail = false,
                 }));


            services.AddScoped<IDistributedCacheProvider, RedisDistributedCachProvider>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

    }
}
