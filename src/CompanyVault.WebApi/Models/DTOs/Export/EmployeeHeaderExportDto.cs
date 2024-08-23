namespace CompanyVault.WebApi.Models.DTOs.Export;

/// <summary>
/// A public DTO that represents an employee.
/// </summary>
public class EmployeeHeaderExportDto
{
    public required string EmployeeNumber { get; set; }
    public required string FullName { get; set; }
}
