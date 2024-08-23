namespace CompanyVault.WebApi.Models.DTOs.Import;

/// <summary>
/// A DTO that is used to import a department.
/// </summary>
public class DepartmentImportDto : Department
{
    public required string CompanyCode { get; set; }
}
