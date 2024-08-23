using CompanyVault.WebApi.Models;
using CompanyVault.WebApi.Models.DTOs.Export;

namespace CompanyVault.WebApi.Repositories.Abstractions;

/// <summary>
/// Defines the contract for the employee repository.
/// </summary>
public interface IEmployeeRepository
{
    Task AddAsync(IEnumerable<Employee> employees, CancellationToken cancellationToken);

    Task<EmployeeExportDto> GetAsync(int companyId, string number, CancellationToken cancellationToken);
}
