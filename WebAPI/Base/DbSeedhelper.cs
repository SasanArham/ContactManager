using Application.Base;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Base
{
    public static class DbSeedhelper
    {
        public static void MigrateDataBase(this WebApplication app)
        {
            Console.WriteLine("Trying to migrate db if required");
            try
            {
                using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<IDatabaseContext>();
                    context!.Database.Migrate();
                }
                Console.WriteLine("Migrated successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to migrate");
                throw;
            }
        }
    }
}
