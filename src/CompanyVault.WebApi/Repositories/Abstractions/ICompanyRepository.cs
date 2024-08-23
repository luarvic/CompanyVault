using CompanyVault.WebApi.Models.DTOs.Export;

namespace CompanyVault.WebApi.Repositories.Abstractions;

/// <summary>
/// Define the contract for the company repository.
/// </summary>
public interface ICompanyRepository
{
    Task RemoveAsync(CancellationToken cancellationToken);
    Task<List<CompanyHeaderExportDto>> GetAsync(CancellationToken cancellationToken);
    Task<CompanyExportDto> GetAsync(int id, CancellationToken cancellationToken);
}
