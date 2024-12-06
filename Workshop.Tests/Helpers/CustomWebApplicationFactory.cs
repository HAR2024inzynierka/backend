using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Workshop.Core.Interfaces;
using Workshop.Infrastructure.Data;
using Workshop.Tests.Helpers;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<WorkshopDbContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<WorkshopDbContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb");
            });

            var serviceProvider = services.BuildServiceProvider();
            using (var scope = serviceProvider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<WorkshopDbContext>();
                var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasherService>();

                db.Database.EnsureCreated();

                TestDataFactory.InitializeTestData(db, passwordHasher);
            }
        });
    }
}
