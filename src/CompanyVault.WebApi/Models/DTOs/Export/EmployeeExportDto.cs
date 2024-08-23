namespace CompanyVault.WebApi.Models.DTOs.Export
{
    /// <summary>
    /// A public DTO that represents an employee with its managers.
    /// </summary>
    public class EmployeeExportDto : EmployeeHeaderExportDto
    {
        public required string Email { get; set; }
        public required string Department { get; set; }
        public required DateOnly HireDate { get; set; }
        // Ordered ascending by seniority (i.e. the immediate manager first).
        public List<EmployeeHeaderExportDto>? Managers { get; set; }
    }
}
