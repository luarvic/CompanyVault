using CompanyVault.WebApi.Repositories.Abstractions;

namespace CompanyVault.WebApi.Repositories.Implementations;

/// <summary>
/// Implements the unit of work that manages the repositories.
/// </summary>
public class UnitOfWork(CompanyVaultDbContext context) : IUnitOfWork, IDisposable
{
    private bool disposed = false;

    private ICompanyRepository? _companies;
    private IEmployeeRepository? _employees;

    public ICompanyRepository Companies
    {
        get
        {
            _companies ??= new CompanyRepository(context);
            return _companies;
        }
    }

    public IEmployeeRepository Employees
    {
        get
        {
            _employees ??= new EmployeeRepository(context);
            return _employees;
        }
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }
        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
