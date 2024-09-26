using CompanyVault.WebApi.Models.DTOs.Export;
using CompanyVault.WebApi.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CompanyVault.WebApi.Repositories.Implementations;

/// <summary>
/// Implements the company repository.
/// </summary>
public class CompanyRepository(CompanyVaultDbContext dbContext) : ICompanyRepository
{
    private IQueryable<CompanyDepartmentExportDto> GetCompanyDepartmentPairs()
    {
        return dbContext.Companies
            .GroupJoin(dbContext.Departments, c => c.Id, d => d.CompanyId, (c, d) => new { Company = c, Department = d })
            .SelectMany(g => g.Department.DefaultIfEmpty(), (c, d) => new CompanyDepartmentExportDto
            {
                Company = c.Company,
                Department = d
            })
            ;
    }

    private IQueryable<CompanyEmployeeExportDto> GetCompanyEmployeePairs()
    {
        return GetCompanyDepartmentPairs()
            .GroupJoin(dbContext.Employees, c => c.Department!.Id, e => e.DepartmentId, (c, e) => new { Company = c.Company, Employee = e })
            .SelectMany(c => c.Employee.DefaultIfEmpty(), (c, e) => new CompanyEmployeeExportDto
            {
                Company = c.Company,
                Employee = e
            });
    }

    public async Task<List<CompanyHeaderExportDto>> GetAsync(CancellationToken cancellationToken)
    {
        return await GetCompanyEmployeePairs()
        .GroupBy(c => c.Company)
        .Select(g => new CompanyHeaderExportDto
        {
            Id = g.Key.Id,
            Code = g.Key.Code,
            Description = g.Key.Description,
            EmployeeCount = g.Count(g => g.Employee != null)
        }).ToListAsync(cancellationToken);
    }

    public async Task<CompanyExportDto> GetAsync(int id, CancellationToken cancellationToken)
    {
        return await GetCompanyEmployeePairs()
            .GroupBy(c => c.Company)
            .Select(g => new
            CompanyExportDto
            {
                Id = g.Key.Id,
                Code = g.Key.Code,
                Description = g.Key.Description,
                EmployeeCount = g.Count(),
                Employees = g
                    .Where(x => x.Employee != null)
                    .Select(x => new EmployeeHeaderExportDto
                    {
                        EmployeeNumber = x.Employee!.Number,
                        FullName = $"{x.Employee.FirstName} {x.Employee.LastName}"
                    }).ToList()
            })
            .FirstAsync(c => c.Id == id, cancellationToken);
    }

    public async Task RemoveAsync(CancellationToken cancellationToken)
    {
        await dbContext.Companies.ExecuteDeleteAsync(cancellationToken);
    }
}
