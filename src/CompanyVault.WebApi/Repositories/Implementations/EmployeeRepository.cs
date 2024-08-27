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
        const string query = """
            WITH Managers(EmployeeId, ManagerId, EmployeeNumber, FullName, EmployeeLevel) AS
            (
                SELECT
                    Id AS EmployeeId,
                    ManagerId,
                    Number AS EmployeeNumber,
                    FirstName || ' ' || LastName AS FullName,
                    0 AS EmployeeLevel
                FROM Employees
                WHERE
                    Id = {0}
                UNION ALL
                SELECT
                    e.Id AS EmployeeId,
                    e.ManagerId,
                    e.Number AS EmployeeNumber,
                    e.FirstName || ' ' || e.LastName AS FullName,
                    m.EmployeeLevel + 1
                FROM Employees e
                JOIN Managers m ON e.Id = m.ManagerId
            )
            SELECT EmployeeNumber, FullName
            FROM Managers
            ORDER BY EmployeeLevel ASC;
            """;

        var employee = await dbContext.Employees
            .Include(e => e.Department)
            .Where(e => e.Number == number && e.Department.Company.Id == companyId)
            .FirstAsync(cancellationToken);

        var managers = employee.ManagerId == null
        ? []
        : dbContext.Database.SqlQueryRaw<EmployeeHeaderExportDto>(string.Format(query, employee.ManagerId)).ToList();

        return new EmployeeExportDto()
        {
            EmployeeNumber = employee.Number,
            FullName = $"{employee.FirstName} {employee.LastName}",
            Email = employee.Email,
            Department = employee.Department.Name,
            HireDate = employee.HireDate,
            Managers = managers
        };
    }
}
