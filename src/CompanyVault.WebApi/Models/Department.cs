namespace CompanyVault.WebApi.Models;

/// <summary>
/// Represents a department entity.
/// </summary>
public class Department
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required Company Company { get; set; }
    public required List<Employee> Employees { get; set; }
}
