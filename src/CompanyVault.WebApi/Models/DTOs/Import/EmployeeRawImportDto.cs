using AutoMapper;

namespace CompanyVault.WebApi.Models.DTOs.Import;

/// <summary>
/// A DTO that is used to import employee data from a CSV file.
/// The SCV file column names must be equal to the properties of this class.
///
/// A file sample:
/// CompanyId,CompanyCode,CompanyDescription,EmployeeNumber,EmployeeFirstName,EmployeeLastName,EmployeeEmail,EmployeeDepartment,HireDate,ManagerEmployeeNumber
/// 5,Whiskey,Whiskey Description, E196582, Free, Alderman, falderman0@dot.gov,Accounting,,
/// 3,Zulu,Zulu Description, E173260, Jacinthe, Seczyk, jseczyk1@gizmodo.com,Human Resources,2021-01-11,E143607
/// 2,Delta,Delta Description, E175521, Nicolas, Loos, nloos2@a8.net,Engineering,2005-08-30,E156564
/// </summary>
[AutoMap(typeof(Company))]
[AutoMap(typeof(Department))]
[AutoMap(typeof(Employee))]
public record EmployeeRawImportDto
{
    public required int CompanyId { get; set; }
    public required string CompanyCode { get; set; }
    public required string CompanyDescription { get; set; }
    public required string EmployeeNumber { get; set; }
    public required string EmployeeFirstName { get; set; }
    public required string EmployeeLastName { get; set; }
    public required string EmployeeEmail { get; set; }
    public required string EmployeeDepartment { get; set; }
    public DateOnly? HireDate { get; set; }
    public string? ManagerEmployeeNumber { get; set; }
}
