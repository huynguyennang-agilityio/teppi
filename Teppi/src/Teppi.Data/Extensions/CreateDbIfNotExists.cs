using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Teppi.Data.DbContexts;
using Teppi.Data.Persistence;

namespace Teppi.Data.Extensions;

public static class Extensions
{
    public static void CreateDbIfNotExists(this IHost host)
    {
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<TeppiDbContext>();
                context.Database.EnsureCreated();
                DbInitializer.Initialize(context);
            }
        }
    }
}



