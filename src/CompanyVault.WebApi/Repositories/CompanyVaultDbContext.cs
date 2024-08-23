using CompanyVault.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyVault.WebApi.Repositories;

/// <summary>
/// Represents the database context.
/// </summary>
public class CompanyVaultDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    private readonly string _dbPath;

    public DbSet<Company> Companies { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Employee> Employees { get; set; }

    public CompanyVaultDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
        var folder = Environment.CurrentDirectory;
        _dbPath = Path.Join(folder, "CompanyVault.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={_dbPath}")
            .LogTo(Console.WriteLine,
                _configuration.GetSection("Logging:LogLevel:Microsoft.EntityFrameworkCore").Get<LogLevel>());
}
