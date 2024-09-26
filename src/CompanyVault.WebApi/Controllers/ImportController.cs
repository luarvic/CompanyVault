using CompanyVault.WebApi.Models.DTOs.Import;
using CompanyVault.WebApi.Repositories.Abstractions;
using CompanyVault.WebApi.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CompanyVault.WebApi.Controllers;

/// <summary>
/// Implements import endpoint that imports employee data from CSV.
/// </summary>
[ApiController]
[Route("/")]
public class ImportController(ICsvParserService csvParser,
    IEmployeeMapperService employeeMapper,
    IUnitOfWork unitOfWork) : ControllerBase
{
    /// <summary>
    /// Imports employee data from CSV.
    /// </summary>
    /// <param name="csv">Data in CSV format. First line must contain column names.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns></returns>
    [HttpPost("DataStore")]
    [Consumes("text/csv")]
    public async Task<IActionResult> ImportAsync([FromBody] string csv, CancellationToken cancellationToken)
    {
        var rawRecords = csvParser.Parse<EmployeeRawImportDto>(csv);
        var employees = employeeMapper.Map(rawRecords, cancellationToken);
        await unitOfWork.Companies.RemoveAsync(cancellationToken);
        await unitOfWork.Employees.AddAsync(employees, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Ok();
    }
}
