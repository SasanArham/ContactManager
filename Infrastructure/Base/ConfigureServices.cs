﻿using Application.Base;
using Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Application.Common;
using Infrastructure.Common;
using StackExchange.Redis;
using MassTransit;
using System.Reflection;
using Application.Base.Exceptions;

namespace Infrastructure.Base
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMainDatabase(configuration);
            services.AddDistributedCacheDatabase(configuration);
            services.AddMassTransit(configuration);

            services.AddScoped<IDistributedCacheProvider, RedisDistributedCachProvider>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        private static IServiceCollection AddMainDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var ServerName = Environment.GetEnvironmentVariable("DATABASE_SERVER") ?? configuration["MainDataBase:ServerName"];
            var Port = Environment.GetEnvironmentVariable("DATABASE_PORT") ?? configuration["MainDataBase:Port"];
            var DatabaseName = Environment.GetEnvironmentVariable("DATABASE_NAME") ?? configuration["MainDataBase:DbName"];
            var Username = Environment.GetEnvironmentVariable("DATABASE_USER") ?? configuration["MainDataBase:Username"];
            var Password = Environment.GetEnvironmentVariable("DATABASE_PASSWORD") ?? configuration["MainDataBase:MyStrngPassw0rd"];
            string connectionString = $"Server={ServerName},{Port};Database={DatabaseName};User Id={Username};Password={Password};TrustServerCertificate=Yes;MultipleActiveResultSets=true";
            Console.WriteLine("the connection is : " + connectionString);

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
    }
}
