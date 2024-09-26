namespace CompanyVault.WebApi.Models.DTOs.Export;

/// <summary>
/// A DTO that represents a (company, employee) pair.
/// </summary>
public class CompanyEmployeeExportDto
{
    public required Company Company { get; set; }
    public Employee? Employee { get; set; }
}
