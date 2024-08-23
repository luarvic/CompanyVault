namespace CompanyVault.WebApi.Models;

/// <summary>
/// Represents an employee entity.
/// </summary>
public class Employee
{
    public int Id { get; set; }
    public required string Number { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required DateOnly HireDate { get; set; }
    public required Department Department { get; set; }
    public Employee? Manager { get; set; }
}
