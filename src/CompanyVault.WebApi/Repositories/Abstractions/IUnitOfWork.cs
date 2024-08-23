namespace CompanyVault.WebApi.Repositories.Abstractions;

/// <summary>
/// Defines the contract for the unit of work.
/// </summary>
public interface IUnitOfWork
{
    ICompanyRepository Companies { get; }
    IEmployeeRepository Employees { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
