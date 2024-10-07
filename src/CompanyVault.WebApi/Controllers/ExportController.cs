using CompanyVault.WebApi.Models.DTOs.Export;
using CompanyVault.WebApi.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CompanyVault.WebApi.Controllers;

/// <summary>
/// Implements export endpoints that return company and employee data.
/// </summary>
[ApiController]
[Route("/")]
public class ExportController(IUnitOfWork unitOfWork) : ControllerBase
{
    /// <summary>
    /// Returns list of companies.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>List of companies.</returns>
    [HttpGet("Companies")]
    public async Task<ActionResult<List<CompanyHeaderExportDto>>> ExportCompaniesAsync(CancellationToken cancellationToken)
    {
        var result = await unitOfWork.Companies.GetAsync(cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Returns company by its id.
    /// </summary>
    /// <param name="companyId">Company id.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Company.</returns>
    [HttpGet("Companies/{companyId}")]
    public async Task<ActionResult<CompanyExportDto>> ExportCompaniesAsync(int companyId, CancellationToken cancellationToken)
    {
        var result = await unitOfWork.Companies.GetAsync(companyId, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Returns employee by its number and company id.
    /// </summary>
    /// <param name="companyId">Company id.</param>
    /// <param name="employeeNumber">Employee number.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Employee.</returns>
    [HttpGet("Companies/{companyId}/Employees/{employeeNumber}")]
    public async Task<ActionResult<CompanyExportDto>> ExportCompaniesAsync(int companyId, string employeeNumber, CancellationToken cancellationToken)
    {
        var result = await unitOfWork.Employees.GetAsync(companyId, employeeNumber, cancellationToken);
        return Ok(result);
    }
}
