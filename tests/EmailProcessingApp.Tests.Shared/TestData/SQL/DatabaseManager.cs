using EmailProcessingApp.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EmailProcessingApp.Tests.Shared.TestData.SQL
{
    public static class DatabaseManager
    {
        public static void InitializeDatabase(WebApplicationFactory<Program> factory)
        {
            var scopeFactory = factory.Services.GetService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureDeleted();

                context.Database.EnsureCreated();

                context.Database.Migrate();
            }
        }
    }
}
