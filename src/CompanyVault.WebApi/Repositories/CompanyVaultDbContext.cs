using CompanyVault.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyVault.WebApi.Repositories;

/// <summary>
/// Represents the database context.
/// </summary>
public class CompanyVaultDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Company> Companies { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Employee> Employees { get; set; }
}
