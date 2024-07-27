using Data;
using Data.DataSeeding;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IntegrationTests;
public class ApiApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<DataContext>();

            services.AddDatabase("DataSource=:memory:");

            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
            seeder.ApplySeeding();
        });
    }
}
