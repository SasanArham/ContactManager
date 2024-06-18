using Application.Base;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Application.Common;
using Infrastructure.Common;
using StackExchange.Redis;
using MassTransit;
using System.Reflection;
using Application.Base.Exceptions;
using Infrastructure.Common.FileManagement;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Base
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMainDatabase(configuration);
            services.AddDistributedCacheDatabase(configuration);
            services.AddMassTransit(configuration);
            services.ConfigAzureApp(configuration);

            services.AddScoped<IDistributedCacheProvider, RedisDistributedCachProvider>();
            services.AddScoped<IFileManager, AzureFileManager>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        private static IServiceCollection AddMainDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = SQlDbConnectionStringHelper.GetConnectionString(configuration);
            services.AddDbContext<DatabaseContext>(
                (sp, options) =>
                {
                    options.UseLazyLoadingProxies();
                    options.UseSqlServer(connectionString, b => b.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName));

                });
            services.AddScoped<IDatabaseContext>(provider => provider.GetRequiredService<DatabaseContext>());
            return services;
        }

        private static IServiceCollection AddDistributedCacheDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var ServerName = Environment.GetEnvironmentVariable("CACHE_DATABASE_SERVER") ?? configuration["DistributedCacheDataBase:ServerName"];
            var Port = Environment.GetEnvironmentVariable("CACHE_SERVER_PORT") ?? configuration["DistributedCacheDataBase:Port"];
            var redisConnection = $"{ServerName}:{Port}";

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
            return services;
        }

        private static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            var hostAddress = Environment.GetEnvironmentVariable("EVENT_BUS_HOST_ADDRESS") ?? configuration["EVENT_BUS:HOST_ADDRESS"];

            services.AddMassTransit(config =>
            {
                config.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("contact-manager", false));
                var entryAssembly = Assembly.GetAssembly(typeof(CrudException));
                config.AddConsumers(entryAssembly);
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.ConfigureEndpoints(ctx);
                    cfg.Host(hostAddress);
                });
                config.AddEntityFrameworkOutbox<DatabaseContext>(options =>
                {
                    options.QueryDelay = TimeSpan.FromSeconds(30);
                    options.UseSqlServer();
                    options.UseBusOutbox();
                });
            });

            return services;
        }

        private static IServiceCollection ConfigAzureApp(this IServiceCollection services, IConfiguration configuration)
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") is null)
            {
                Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", configuration["AzureApp:ASPNETCORE_ENVIRONMENT"]);
            }
            if (Environment.GetEnvironmentVariable("AZURE_CLIENT_ID") is null)
            {
                Environment.SetEnvironmentVariable("AZURE_CLIENT_ID", configuration["AzureApp:AZURE_CLIENT_ID"]);
            }
            if (Environment.GetEnvironmentVariable("AZURE_TENANT_ID") is null)
            {
                Environment.SetEnvironmentVariable("AZURE_TENANT_ID", configuration["AzureApp:AZURE_TENANT_ID"]);
            }
            if (Environment.GetEnvironmentVariable("AZURE_CLIENT_SECRET") is null)
            {
                Environment.SetEnvironmentVariable("AZURE_CLIENT_SECRET", configuration["AzureApp:AZURE_CLIENT_SECRET"]);
            }

            services.Configure<AzureStorageConfigs>((config) =>
            {
                config.DefaultContainer = configuration["AzureStorageConfigs:DefaultContainer"]!;
                config.AccountName = configuration["AzureStorageConfigs:AccountName"]!;
            });

            return services;
        }
    }
}
