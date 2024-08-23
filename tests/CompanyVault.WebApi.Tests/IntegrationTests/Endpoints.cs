using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace CompanyVault.WebApi.Tests.IntegrationTests;

public class IntegrationTests(CustomWebApplicationFactory<Program> applicationFactory) : IClassFixture<CustomWebApplicationFactory<Program>>
{
    [Fact]
    public async Task PostDataStoreEndpoint_WhenValidCsvDataPassed_ShouldSucceed()
    {
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
        var response = await client.SendAsync(request);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task PostDataStoreEndpoint_WhenEmployeeNumberIsNotUniqueWithinCompany_ShouldFail()
    {
        const string csv = @"
CompanyId,CompanyCode,CompanyDescription,EmployeeNumber,EmployeeFirstName,EmployeeLastName,EmployeeEmail,EmployeeDepartment,HireDate,ManagerEmployeeNumber
5,Whiskey,Whiskey Description,E196582,Free,Alderman,falderman0@dot.gov,Accounting,,
3,Zulu,Zulu Description,E173260,Jacinthe,Seczyk,jseczyk1@gizmodo.com,Human Resources,2021-01-11,
2,Delta,Delta Description,E114960,Nicolas,Loos,nloos2@a8.net,Engineering,2005-08-30,
2,Delta,Delta Description,E114960,Theresita,Mathiot,tmathiot3@goo.gl,Business Development,2012-12-11,E175521
";
        var client = applicationFactory.CreateClient();
        var request = new HttpRequestMessage(HttpMethod.Post, "DataStore")
        {
            Content = new StringContent(csv, Encoding.UTF8, new MediaTypeHeaderValue("text/csv"))
        };
        var response = await client.SendAsync(request);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PostDataStoreEndpoint_WhenEmployeesFromDifferentCompaniesHaveSameNumber_ShouldSucceed()
    {
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
        var response = await client.SendAsync(request);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
