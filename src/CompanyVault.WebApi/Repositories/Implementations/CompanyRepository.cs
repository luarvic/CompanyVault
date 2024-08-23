using CompanyVault.WebApi.Models.DTOs.Export;
using CompanyVault.WebApi.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CompanyVault.WebApi.Repositories.Implementations;

/// <summary>
/// Implements the company repository.
/// </summary>
public class CompanyRepository(CompanyVaultDbContext dbContext) : ICompanyRepository
{
    public async Task<List<CompanyHeaderExportDto>> GetAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Employees
            .GroupBy(e => e.Department.Company)
            .Select(g => new
            CompanyHeaderExportDto
            {
                Id = g.Key.Id,
                Code = g.Key.Code,
                Description = g.Key.Description,
                EmployeeCount = g.Count()
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<CompanyExportDto> GetAsync(int id, CancellationToken cancellationToken)
    {
        return await dbContext.Employees
            .Where(e => e.Department.Company.Id == id)
            .GroupBy(e => e.Department.Company)
            .Select(g => new
            CompanyExportDto
            {
                Id = g.Key.Id,
                Code = g.Key.Code,
                Description = g.Key.Description,
                EmployeeCount = g.Count(),
                Employees = g.Select(e => new EmployeeHeaderExportDto
                {
                    EmployeeNumber = e.Number,
                    FullName = $"{e.FirstName} {e.LastName}"
                }).ToList()
            })
            .SingleAsync(cancellationToken);
    }

    public async Task RemoveAsync(CancellationToken cancellationToken)
    {
        await dbContext.Companies.ExecuteDeleteAsync(cancellationToken);
    }
}
