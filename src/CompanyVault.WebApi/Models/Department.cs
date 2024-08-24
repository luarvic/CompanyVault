using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CompanyVault.WebApi.Models;

/// <summary>
/// Represents a department entity.
/// </summary>
[Index(nameof(CompanyId))]
public class Department
{
    public required int Id { get; set; }
    [MaxLength(100)]
    public required string Name { get; set; }
    public required int CompanyId { get; set; }
    public required Company Company { get; set; }
    public required List<Employee> Employees { get; set; }
}
