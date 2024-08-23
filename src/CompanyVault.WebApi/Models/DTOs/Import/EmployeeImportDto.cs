using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyVault.WebApi.Models.DTOs.Import;

/// <summary>
/// A DTO that is used to import an employee.
/// </summary>
public class EmployeeImportDto : Employee
{
    [NotMapped]
    public required string CompanyCode { get; set; }
    [NotMapped]
    public required string DepartmentCode { get; set; }
    [NotMapped]
    public string? ManagerCode { get; set; }
}
