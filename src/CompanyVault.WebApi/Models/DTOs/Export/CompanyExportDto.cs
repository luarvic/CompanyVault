namespace CompanyVault.WebApi.Models.DTOs.Export;

/// <summary>
/// A public DTO that represents a company with its employees.
/// </summary>
public class CompanyExportDto : CompanyHeaderExportDto
{
    public List<EmployeeHeaderExportDto>? Employees { get; set; }
}
