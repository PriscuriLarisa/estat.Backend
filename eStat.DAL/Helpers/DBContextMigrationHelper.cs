using eStat.DAL.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace eStat.DAL.Helpers
{
    public static class DBContextMigrationHelper
    {
        public static async Task Migrate(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            using var db = services.GetService<DatabaseContext>();

            if (db is not null)
            {
                await db.Database.MigrateAsync();
            }
        }
    }
}
