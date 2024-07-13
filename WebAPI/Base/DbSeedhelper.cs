using Application.Base;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace WebAPI.Base
{
    public static class DbSeedhelper
    {
        public static void MigrateDataBase(this WebApplication app)
        {
            Log.Information("Trying to migrate db if required");
            try
            {
                using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<IDatabaseContext>();
                    context!.Database.Migrate();
                }
                Log.Information("Migrated successfully");
            }
            catch (Exception ex)
            {
                Log.Fatal("{@Message}{@Exception}", "Failed to migrate", ex);
                throw;
            }
        }
    }
}
