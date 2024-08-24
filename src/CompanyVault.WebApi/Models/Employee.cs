using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CompanyVault.WebApi.Models;

/// <summary>
/// Represents an employee entity.
/// </summary>
[Index(nameof(DepartmentId))]
[Index(nameof(ManagerId))]
[Index(nameof(Number), nameof(DepartmentId))]
public class Employee
{
    public required int Id { get; set; }
    [MaxLength(100)]
    public required string Number { get; set; }
    [MaxLength(100)]
    public required string FirstName { get; set; }
    [MaxLength(100)]
    public required string LastName { get; set; }
    [MaxLength(100)]
    public required string Email { get; set; }
    public DateOnly? HireDate { get; set; }
    public required int DepartmentId { get; set; }
    public required Department Department { get; set; }
    public int? ManagerId { get; set; }
    public Employee? Manager { get; set; }
}
