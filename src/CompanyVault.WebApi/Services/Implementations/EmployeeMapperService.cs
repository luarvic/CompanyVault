using AutoMapper;
using CompanyVault.WebApi.Models;
using CompanyVault.WebApi.Models.DTOs.Import;
using CompanyVault.WebApi.Services.Abstractions;

namespace CompanyVault.WebApi.Services.Implementations;

/// <summary>
/// Implements the employee mapper service that is responsible
/// for mapping raw EmployeeRawImportDto objects to Employee entities.
/// </summary>
public class EmployeeMapperService(IMapper mapper) : IEmployeeMapperService
{
    public IEnumerable<Employee> Map(IEnumerable<EmployeeRawImportDto> records, CancellationToken cancellationToken)
    {
        var companies = new Dictionary<string, CompanyImportDto>();
        var departments = new Dictionary<string, DepartmentImportDto>();
        var employees = new Dictionary<string, EmployeeImportDto>();

        // Map records to entities.
        foreach (var record in records)
        {
            // If request is cancelled, stop processing.
            if (cancellationToken.IsCancellationRequested)
            {
                break;
            }

            var company = mapper.Map<CompanyImportDto>(record);
            var department = mapper.Map<DepartmentImportDto>(record);
            var employee = mapper.Map<EmployeeImportDto>(record);

            companies.TryAdd(record.CompanyCode, company);
            departments.TryAdd($"{record.CompanyCode},{record.EmployeeDepartment}", department);
            if (!employees.TryAdd($"{record.CompanyCode},{record.EmployeeNumber}", employee))
            {
                throw new InvalidOperationException($"Employee with number {record.EmployeeNumber} already exists in company {record.CompanyCode}.");
            }

            department.Company = companies[record.CompanyCode];
            employee.Department = departments[$"{record.CompanyCode},{record.EmployeeDepartment}"];
        }

        // Assign managers to employees.
        foreach (var employee in employees.Values.Where(e => e.ManagerCode != ""))
        {
            var managerKey = $"{employee.CompanyCode},{employee.ManagerCode}";
            if (employees.TryGetValue(managerKey, out var manager))
            {
                employee.Manager = manager;
            }
            else
            {
                throw new InvalidOperationException($"Manager with number {employee.ManagerCode} does not exist in company {employee.CompanyCode}.");
            }
        }

        return employees.Values;
    }
}
