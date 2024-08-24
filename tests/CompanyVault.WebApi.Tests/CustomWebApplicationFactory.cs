using System.Data.Common;
using CompanyVault.WebApi.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyVault.WebApi.Tests;

/// <summary>
/// Represents a custom web application factory that mocks infrastructure (e.g. a database).
/// </summary>
public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var dbContextOptionsDescriptor = services.Single(
                d => d.ServiceType ==
                    typeof(DbContextOptions<CompanyVaultDbContext>));
            services.Remove(dbContextOptionsDescriptor);

            var dbContextDescriptor = services.Single(
                d => d.ServiceType ==
                    typeof(CompanyVaultDbContext));
            services.Remove(dbContextDescriptor);

            // Create open SqliteConnection so EF won't automatically close it.
            services.AddSingleton<DbConnection>(container =>
            {
                var connection = new SqliteConnection("DataSource=:memory:");
                connection.Open();
                return connection;
            });

            services.AddDbContext<CompanyVaultDbContext>((container, options) =>
            {
                var connection = container.GetRequiredService<DbConnection>();
                options.UseSqlite(connection);
            });
        });

        builder.UseEnvironment("Test");
    }
}
