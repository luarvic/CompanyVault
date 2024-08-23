using CompanyVault.WebApi.Models;
using CompanyVault.WebApi.Models.DTOs.Import;

namespace CompanyVault.WebApi.Services.Abstractions;

/// <summary>
/// Defines the contract for the employee mapper service that is responsible
/// for mapping raw EmployeeRawImportDto objects to Employee entities.
/// </summary>
public interface IEmployeeMapperService
{
    public IEnumerable<Employee> Map(IEnumerable<EmployeeRawImportDto> records, CancellationToken cancellationToken);
}
