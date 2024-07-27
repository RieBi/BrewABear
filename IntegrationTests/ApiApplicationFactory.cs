using Data;
using Data.DataSeeding;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IntegrationTests;
public class ApiApplicationFactory : WebApplicationFactory<Program>
{
    private bool _disposed = false;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<DataContext>();
            services.RemoveAll<DbContextOptions<DataContext>>();

            var connectionString = $"DataSource={Guid.NewGuid()};";
            services.AddDatabase(connectionString);

            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
            seeder.ApplySeeding();
        });
    }

    protected override void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
        {
            using var scope = Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DataContext>();
            context.Database.EnsureDeleted();

            _disposed = true;
        }

        base.Dispose(disposing);
    }
}
