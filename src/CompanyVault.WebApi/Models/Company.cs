namespace CompanyVault.WebApi.Models;

/// <summary>
/// Represents a company entity.
/// </summary>
public class Company
{
    public int Id { get; set; }
    public required string Code { get; set; }
    public required string Description { get; set; }
    public required List<Department> Departments { get; set; }
}
