using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CompanyVault.WebApi.Models;

/// <summary>
/// Represents a company entity.
/// </summary>
[Index(nameof(Code), IsUnique = true)]
public class Company
{
    public required int Id { get; set; }
    [MaxLength(100)]
    public required string Code { get; set; }
    [MaxLength(200)]
    public required string Description { get; set; }
    public required List<Department> Departments { get; set; }
}
