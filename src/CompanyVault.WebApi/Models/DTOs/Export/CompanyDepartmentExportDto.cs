namespace CompanyVault.WebApi.Models.DTOs.Export;

/// <summary>
/// A DTO that represents a (company, department) pair.
/// </summary>
public class CompanyDepartmentExportDto
{
    public required Company Company { get; set; }
    public Department? Department { get; set; }
}
