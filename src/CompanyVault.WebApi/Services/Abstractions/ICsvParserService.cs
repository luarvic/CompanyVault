namespace CompanyVault.WebApi.Services.Abstractions;

/// <summary>
/// Defines the contract for the CSV parser responsible for parsing CSV strings into objects.
/// </summary>
public interface ICsvParserService
{
    IEnumerable<T> Parse<T>(string csv);
}
