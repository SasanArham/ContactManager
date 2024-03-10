using Infrastructure.Base;
using Repository.Base;
using Application.Base;
using Domain.Base;
using WebAPI.Base;
using WebAPI.Base.Middlewares;
using Microsoft.EntityFrameworkCore;


namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.ConfigControllers();
            builder.Services.ConfigSwagger();

            builder.Services.AddDomainServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddRepositories(builder.Configuration);
            builder.Services.AddApplicationServices();

            builder.Services.ConfigCors();
            builder.Services.AddHttpContextAccessor();


            var app = builder.Build();
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseHttpsRedirection();
            app.UseCors("ClientPermission");
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.MigrateDataBase();

            app.Run();
        }

    }

    
}