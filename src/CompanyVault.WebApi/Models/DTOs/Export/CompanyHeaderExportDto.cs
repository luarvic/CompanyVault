namespace CompanyVault.WebApi.Models.DTOs.Export;

/// <summary>
/// A public DTO that represents a company.
/// </summary>
public class CompanyHeaderExportDto
{
    public int Id { get; set; }
    public required string Code { get; set; }
    public required string Description { get; set; }
    public required int EmployeeCount { get; set; }
}