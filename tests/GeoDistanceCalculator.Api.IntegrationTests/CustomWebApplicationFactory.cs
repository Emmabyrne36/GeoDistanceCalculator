using GeoDistanceCalculator.Data;
using GeoDistanceCalculator.Data.Interfaces;
using GeoDistanceCalculator.Data.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GeoDistanceCalculator.Api.IntegrationTests;

public class CustomWebApplicationFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            // Remove the app's ApplicationDbContext registration.
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<DataContext>));


            // Register an in-memory database provider
            services.AddDbContext<DataContext>(options =>
            {
                options.UseInMemoryDatabase("MyInMemoryDb");
            });

            // Register a mock repository
            services.AddScoped<IRepository, MockRepository>();
        });
    }
}