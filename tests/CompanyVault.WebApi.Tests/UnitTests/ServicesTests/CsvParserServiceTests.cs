using CompanyVault.WebApi.Models.DTOs.Import;
using CompanyVault.WebApi.Services.Implementations;

namespace CompanyVault.WebApi.Tests.UnitTests.ServicesTests;

public class CsvParserServiceTests
{
    [Fact]
    public void CsvParserService_WhenPassedValidCsvData_ShouldSucceed()
    {
        // Arrange
        var csv = @"
CompanyId,CompanyCode,CompanyDescription,EmployeeNumber,EmployeeFirstName,EmployeeLastName,EmployeeEmail,EmployeeDepartment,HireDate,ManagerEmployeeNumber
5,Whiskey,Whiskey Description,E196582,Free,Alderman,falderman0@dot.gov,Accounting,,
3,Zulu,Zulu Description,E173260,Jacinthe,Seczyk,jseczyk1@gizmodo.com,Human Resources,2021-01-11,
2,Delta,Delta Description,E175521,Nicolas,Loos,nloos2@a8.net,Engineering,2005-08-30,
4,Oscar,Oscar Description,E114960,Theresita,Mathiot,tmathiot3@goo.gl,Business Development,2012-12-11,
4,Oscar,Oscar Description,E154343,Jens,Aldous,jaldous4@ihg.com,Sales,2020-09-28,E114960
2,Delta,Delta Description,E103708,Kristo,Sudy,ksudy5@state.tx.us,Legal,2014-05-25,
5,Whiskey,Whiskey Description,E166295,Tedra,Hinemoor,thinemoor6@chicagotribune.com,Services,2006-04-06,
8,India,India Description,E161105,Sileas,Toma,stoma7@hugedomains.com,Engineering,2009-06-09,
4,Oscar,Oscar Description,E190180,Tarra,Necrews,tnecrewsc@slideshare.net,Marketing,2015-04-03,E154343
";
        var csvParserService = new CsvParserService();

        // Act
        var result = csvParserService.Parse<EmployeeRawImportDto>(csv);

        // Assert
        Assert.Equal(9, result.ToArray().Length);

        var employeeE196582 = result.Single(r => r.EmployeeNumber == "E196582");
        Assert.Equal("", employeeE196582.ManagerEmployeeNumber);
        Assert.Null(employeeE196582.HireDate);

        var employeeE154343 = result.Single(r => r.EmployeeNumber == "E154343");
        Assert.Equal("E114960", employeeE154343.ManagerEmployeeNumber);
        Assert.Equal(new DateOnly(2020, 9, 28), employeeE154343.HireDate);
    }
}
