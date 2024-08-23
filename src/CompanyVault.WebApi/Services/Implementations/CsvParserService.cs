using System.Globalization;
using System.Text;
using CompanyVault.WebApi.Services.Abstractions;
using CsvHelper;
using CsvHelper.Configuration;

namespace CompanyVault.WebApi.Services.Implementations;

/// <summary>
/// Implements the CSV parser responsible for parsing CSV strings into objects.
/// </summary>
public class CsvParserService() : ICsvParserService
{
    public IEnumerable<T> Parse<T>(string csv)
    {
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(csv));
        using var streamReader = new StreamReader(stream);
        using var csvReader = new CsvReader(streamReader, new CsvConfiguration(CultureInfo.InvariantCulture));
        var records = csvReader.GetRecords<T>();
        foreach (var record in records)
        {
            yield return record;
        }
    }
}
