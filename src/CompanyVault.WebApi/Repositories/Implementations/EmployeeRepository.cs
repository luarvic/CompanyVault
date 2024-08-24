using CompanyVault.WebApi.Models;
using CompanyVault.WebApi.Models.DTOs.Export;
using CompanyVault.WebApi.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CompanyVault.WebApi.Repositories.Implementations;

/// <summary>
/// Implements the employee repository.
/// </summary>
public class EmployeeRepository(CompanyVaultDbContext dbContext) : IEmployeeRepository
{
    public async Task AddAsync(IEnumerable<Employee> employees, CancellationToken cancellationToken)
    {
        await dbContext.Employees.AddRangeAsync(employees, cancellationToken);
    }

    public async Task<EmployeeExportDto> GetAsync(int companyId, string number, CancellationToken cancellationToken)
    {
        var employee = await dbContext.Employees
            .Include(e => e.Department)
            .Include(e => e.Manager)
            .Where(e => e.Number == number && e.Department.Company.Id == companyId)
            .FirstAsync(cancellationToken);

        var managers = new List<Employee>();
        var manager = employee.Manager;
        while (manager != null)
        {
            managers.Add(manager);
            dbContext.Entry(manager).Reference(m => m.Manager).Load();
            manager = manager.Manager;
        }

        return new EmployeeExportDto()
        {
            EmployeeNumber = employee.Number,
            FullName = $"{employee.FirstName} {employee.LastName}",
            Email = employee.Email,
            Department = employee.Department.Name,
            HireDate = employee.HireDate,
            Managers = managers.Select(m => new EmployeeHeaderExportDto
            {
                EmployeeNumber = m.Number,
                FullName = $"{m.FirstName} {m.LastName}"
            }).ToList()
        };
    }
}
