using System.Net;
using System.Net.Http.Headers;
using System.Text;
using CompanyVault.WebApi.Models.DTOs.Export;
using Newtonsoft.Json;

namespace CompanyVault.WebApi.Tests.IntegrationTests;

public class EndpointsTests(CustomWebApplicationFactory<Program> applicationFactory) : IClassFixture<CustomWebApplicationFactory<Program>>
{
    [Fact]
    public async Task PostDataStoreEndpoint_WhenValidCsvDataPassed_ShouldSucceed()
    {
        // Arrange
        const string csv = @"
CompanyId,CompanyCode,CompanyDescription,EmployeeNumber,EmployeeFirstName,EmployeeLastName,EmployeeEmail,EmployeeDepartment,HireDate,ManagerEmployeeNumber
5,Whiskey,Whiskey Description,E196582,Free,Alderman,falderman0@dot.gov,Accounting,,
3,Zulu,Zulu Description,E173260,Jacinthe,Seczyk,jseczyk1@gizmodo.com,Human Resources,2021-01-11,
2,Delta,Delta Description,E175521,Nicolas,Loos,nloos2@a8.net,Engineering,2005-08-30,
2,Delta,Delta Description,E114960,Theresita,Mathiot,tmathiot3@goo.gl,Business Development,2012-12-11,E175521
";
        var client = applicationFactory.CreateClient();
        var request = new HttpRequestMessage(HttpMethod.Post, "DataStore")
        {
            Content = new StringContent(csv, Encoding.UTF8, new MediaTypeHeaderValue("text/csv"))
        };

        // Act
        var response = await client.SendAsync(request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task PostDataStoreEndpoint_WhenEmployeesFromDifferentCompaniesHaveSameNumber_ShouldSucceed()
    {
        // Arrange
        const string csv = @"
CompanyId,CompanyCode,CompanyDescription,EmployeeNumber,EmployeeFirstName,EmployeeLastName,EmployeeEmail,EmployeeDepartment,HireDate,ManagerEmployeeNumber
5,Whiskey,Whiskey Description,E196582,Free,Alderman,falderman0@dot.gov,Accounting,,
3,Zulu,Zulu Description,E114960,Jacinthe,Seczyk,jseczyk1@gizmodo.com,Human Resources,2021-01-11,
2,Delta,Delta Description,E175521,Nicolas,Loos,nloos2@a8.net,Engineering,2005-08-30,
2,Delta,Delta Description,E114960,Theresita,Mathiot,tmathiot3@goo.gl,Business Development,2012-12-11,E175521
";
        var client = applicationFactory.CreateClient();
        var request = new HttpRequestMessage(HttpMethod.Post, "DataStore")
        {
            Content = new StringContent(csv, Encoding.UTF8, new MediaTypeHeaderValue("text/csv"))
        };

        // Act
        var response = await client.SendAsync(request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task PostDataStoreEndpoint_WhenEmployeeCircularDependencyDetected_ShouldFail()
    {
        // Arrange
        const string csv = @"
CompanyId,CompanyCode,CompanyDescription,EmployeeNumber,EmployeeFirstName,EmployeeLastName,EmployeeEmail,EmployeeDepartment,HireDate,ManagerEmployeeNumber
3,Zulu,Zulu Description,E173260,Jacinthe0,Seczyk,jseczyk1@gizmodo.com,Human Resources,2021-01-11,E173262
3,Zulu,Zulu Description,E173261,Jacinthe1,Seczyk,jseczyk1@gizmodo.com,Human Resources,2021-01-11,E173260
3,Zulu,Zulu Description,E173262,Jacinthe2,Seczyk,jseczyk1@gizmodo.com,Human Resources,2021-01-11,E173261
";
        var client = applicationFactory.CreateClient();
        var request = new HttpRequestMessage(HttpMethod.Post, "DataStore")
        {
            Content = new StringContent(csv, Encoding.UTF8, new MediaTypeHeaderValue("text/csv"))
        };

        // Act
        var response = await client.SendAsync(request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PostCompaniesEndpoint_WhenCalled_ShouldSucceed()
    {
        // Arrange
        const string csv = @"
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
        var client = applicationFactory.CreateClient();
        var request = new HttpRequestMessage(HttpMethod.Post, "DataStore")
        {
            Content = new StringContent(csv, Encoding.UTF8, new MediaTypeHeaderValue("text/csv"))
        };
        await client.SendAsync(request);

        // Act
        request = new HttpRequestMessage(HttpMethod.Get, "Companies");
        var response = await client.SendAsync(request);
        var companiesJson = await response.Content.ReadAsStringAsync();
        var companies = JsonConvert.DeserializeObject<List<CompanyExportDto>>(companiesJson);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(5, companies?.Count);
        Assert.Equal(3, companies?.Single(c => c.Id == 4).EmployeeCount);
    }
}

// TODO: Add more integration tests for the remaining endpoints.