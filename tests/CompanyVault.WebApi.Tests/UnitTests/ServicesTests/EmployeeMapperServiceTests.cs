using AutoMapper;
using CompanyVault.WebApi.Models.DTOs.Import;
using CompanyVault.WebApi.Services.Implementations;
using NSubstitute;

namespace CompanyVault.WebApi.Tests.UnitTests.ServicesTests;

public class EmployeeMapperServiceTests
{
    [Fact]
    public void EmployeeMapperService_WhenPassedValidDto_ShouldSucceed()
    {
        // Arrange
        var employeeFooRawImportDto = new EmployeeRawImportDto()
        {
            CompanyId = 1,
            CompanyCode = "Zulu",
            CompanyDescription = "Zulu Description",
            EmployeeNumber = "Foo",
            EmployeeFirstName = "FooFirstName",
            EmployeeLastName = "FooLastName",
            EmployeeEmail = "foo@mail.com",
            EmployeeDepartment = "Accounting",
            HireDate = null,
            ManagerEmployeeNumber = "FooNumber"
        };
        var employeeBarRawImportDto = new EmployeeRawImportDto()
        {
            CompanyId = 1,
            CompanyCode = "Zulu",
            CompanyDescription = "Zulu Description",
            EmployeeNumber = "Bar",
            EmployeeFirstName = "BarFirstName",
            EmployeeLastName = "BarLastName",
            EmployeeEmail = "bar@mail.com",
            EmployeeDepartment = "Accounting",
            HireDate = null,
            ManagerEmployeeNumber = ""
        };
        var companyZuluImportDto = new CompanyImportDto()
        {
            Id = 1,
            Code = "Zulu",
            Description = "Zulu Description",
            Departments = []
        };
        var departmentAccountingImportDto = new DepartmentImportDto()
        {
            Name = "Accounting",
            CompanyCode = "Zulu",
            Id = 0,
            CompanyId = 0,
            Company = null,
            Employees = []
        };
        var employeeFooImportDto = new EmployeeImportDto()
        {
            Number = "Foo",
            FirstName = "FooFirstName",
            LastName = "FooLastName",
            Email = "foo@mail.com",
            CompanyCode = "Zulu",
            DepartmentCode = "Accounting",
            HireDate = null,
            ManagerCode = "Bar",
            Id = 0,
            DepartmentId = 0,
            Department = null,
        };
        var employeeBarImportDto = new EmployeeImportDto()
        {
            Number = "Bar",
            FirstName = "BarFirstName",
            LastName = "BarLastName",
            Email = "bar@mail.com",
            CompanyCode = "Zulu",
            DepartmentCode = "Accounting",
            ManagerCode = "",
            Id = 0,
            DepartmentId = 0,
            Department = null,
        };

        var mapper = Substitute.For<IMapper>();
        mapper.Map<CompanyImportDto>(Arg.Is<EmployeeRawImportDto>(e => e.EmployeeNumber == "Foo")).Returns(companyZuluImportDto);
        mapper.Map<DepartmentImportDto>(Arg.Is<EmployeeRawImportDto>(e => e.EmployeeNumber == "Foo")).Returns(departmentAccountingImportDto);
        mapper.Map<EmployeeImportDto>(Arg.Is<EmployeeRawImportDto>(e => e.EmployeeNumber == "Foo")).Returns(employeeFooImportDto);
        mapper.Map<CompanyImportDto>(Arg.Is<EmployeeRawImportDto>(e => e.EmployeeNumber == "Bar")).Returns(companyZuluImportDto);
        mapper.Map<DepartmentImportDto>(Arg.Is<EmployeeRawImportDto>(e => e.EmployeeNumber == "Bar")).Returns(departmentAccountingImportDto);
        mapper.Map<EmployeeImportDto>(Arg.Is<EmployeeRawImportDto>(e => e.EmployeeNumber == "Bar")).Returns(employeeBarImportDto);
        var employeeMapperService = new EmployeeMapperService(mapper);
        List<EmployeeRawImportDto> records = [employeeFooRawImportDto, employeeBarRawImportDto];

        // Act
        var result = employeeMapperService.Map(records, CancellationToken.None);

        // Assert
        Assert.Equal(2, result.ToArray().Length);
        var employeeFoo = result.Single(x => x.Number == "Foo");
        Assert.Equal("Accounting", employeeFoo.Department.Name);
        Assert.Equal("Zulu", employeeFoo.Department.Company.Code);
        Assert.NotNull(employeeFoo.Manager);
        Assert.Equal("Bar", employeeFoo.Manager.Number);
    }

    [Fact]
    public void EmployeeMapperService_WhenEmployeeNumberIsNotUniqueWithinCompany_ShouldFail()
    {
        // Arrange
        var employeeFooRawImportDto = new EmployeeRawImportDto()
        {
            CompanyId = 1,
            CompanyCode = "Zulu",
            CompanyDescription = "Zulu Description",
            EmployeeNumber = "Foo",
            EmployeeFirstName = "FooFirstName",
            EmployeeLastName = "FooLastName",
            EmployeeEmail = "foo@mail.com",
            EmployeeDepartment = "Accounting",
            HireDate = null,
            ManagerEmployeeNumber = "FooNumber"
        };
        var employeeBarRawImportDto = new EmployeeRawImportDto()
        {
            CompanyId = 1,
            CompanyCode = "Zulu",
            CompanyDescription = "Zulu Description",
            EmployeeNumber = "Foo",
            EmployeeFirstName = "BarFirstName",
            EmployeeLastName = "BarLastName",
            EmployeeEmail = "bar@mail.com",
            EmployeeDepartment = "Accounting",
            HireDate = null,
            ManagerEmployeeNumber = ""
        };
        var companyZuluImportDto = new CompanyImportDto()
        {
            Id = 1,
            Code = "Zulu",
            Description = "Zulu Description",
            Departments = []
        };
        var departmentAccountingImportDto = new DepartmentImportDto()
        {
            Name = "Accounting",
            CompanyCode = "Zulu",
            Id = 0,
            CompanyId = 0,
            Company = null,
            Employees = []
        };
        var employeeFooImportDto = new EmployeeImportDto()
        {
            Number = "Foo",
            FirstName = "FooFirstName",
            LastName = "FooLastName",
            Email = "foo@mail.com",
            CompanyCode = "Zulu",
            DepartmentCode = "Accounting",
            HireDate = null,
            ManagerCode = "Bar",
            Id = 0,
            DepartmentId = 0,
            Department = null,
        };
        var employeeBarImportDto = new EmployeeImportDto()
        {
            Number = "Bar",
            FirstName = "BarFirstName",
            LastName = "BarLastName",
            Email = "bar@mail.com",
            CompanyCode = "Zulu",
            DepartmentCode = "Accounting",
            ManagerCode = "",
            Id = 0,
            DepartmentId = 0,
            Department = null,
        };

        var mapper = Substitute.For<IMapper>();
        mapper.Map<CompanyImportDto>(Arg.Is<EmployeeRawImportDto>(e => e.EmployeeNumber == "Foo")).Returns(companyZuluImportDto);
        mapper.Map<DepartmentImportDto>(Arg.Is<EmployeeRawImportDto>(e => e.EmployeeNumber == "Foo")).Returns(departmentAccountingImportDto);
        mapper.Map<EmployeeImportDto>(Arg.Is<EmployeeRawImportDto>(e => e.EmployeeNumber == "Foo")).Returns(employeeFooImportDto);
        mapper.Map<CompanyImportDto>(Arg.Is<EmployeeRawImportDto>(e => e.EmployeeNumber == "Bar")).Returns(companyZuluImportDto);
        mapper.Map<DepartmentImportDto>(Arg.Is<EmployeeRawImportDto>(e => e.EmployeeNumber == "Bar")).Returns(departmentAccountingImportDto);
        mapper.Map<EmployeeImportDto>(Arg.Is<EmployeeRawImportDto>(e => e.EmployeeNumber == "Bar")).Returns(employeeBarImportDto);
        var employeeMapperService = new EmployeeMapperService(mapper);
        List<EmployeeRawImportDto> records = [employeeFooRawImportDto, employeeBarRawImportDto];

        // Act and assert
        var exception = Assert.Throws<InvalidOperationException>(() => employeeMapperService.Map(records, CancellationToken.None));
        Assert.Contains("Employee with number Foo already exists in company Zulu.", exception.Message);
    }

    [Fact]
    public void EmployeeMapperService_WhenManagerIsFromDifferentCompany_ShouldFail()
    {
        // Arrange
        var employeeFooRawImportDto = new EmployeeRawImportDto()
        {
            CompanyId = 1,
            CompanyCode = "Zulu",
            CompanyDescription = "Zulu Description",
            EmployeeNumber = "Foo",
            EmployeeFirstName = "FooFirstName",
            EmployeeLastName = "FooLastName",
            EmployeeEmail = "foo@mail.com",
            EmployeeDepartment = "Accounting",
            HireDate = null,
            ManagerEmployeeNumber = "FooNumber"
        };
        var employeeBarRawImportDto = new EmployeeRawImportDto()
        {
            CompanyId = 2,
            CompanyCode = "Delta",
            CompanyDescription = "Delta Description",
            EmployeeNumber = "Bar",
            EmployeeFirstName = "BarFirstName",
            EmployeeLastName = "BarLastName",
            EmployeeEmail = "bar@mail.com",
            EmployeeDepartment = "Accounting",
            HireDate = null,
            ManagerEmployeeNumber = ""
        };
        var companyZuluImportDto = new CompanyImportDto()
        {
            Id = 1,
            Code = "Zulu",
            Description = "Zulu Description",
            Departments = []
        };
        var companyDeltaImportDto = new CompanyImportDto()
        {
            Id = 2,
            Code = "Delta",
            Description = "Delta Description",
            Departments = []
        };
        var departmentAccountingImportDto = new DepartmentImportDto()
        {
            Name = "Accounting",
            CompanyCode = "Zulu",
            Id = 0,
            CompanyId = 0,
            Company = null,
            Employees = []
        };
        var departmentEngineeringImportDto = new DepartmentImportDto()
        {
            Name = "Engineering",
            CompanyCode = "Delta",
            Id = 0,
            CompanyId = 0,
            Company = null,
            Employees = []
        };
        var employeeFooImportDto = new EmployeeImportDto()
        {
            Number = "Foo",
            FirstName = "FooFirstName",
            LastName = "FooLastName",
            Email = "foo@mail.com",
            CompanyCode = "Zulu",
            DepartmentCode = "Accounting",
            HireDate = null,
            ManagerCode = "Bar",
            Id = 0,
            DepartmentId = 0,
            Department = null,
        };
        var employeeBarImportDto = new EmployeeImportDto()
        {
            Number = "Bar",
            FirstName = "BarFirstName",
            LastName = "BarLastName",
            Email = "bar@mail.com",
            CompanyCode = "Delta",
            DepartmentCode = "Engineering",
            ManagerCode = "",
            Id = 0,
            DepartmentId = 0,
            Department = null,
        };

        var mapper = Substitute.For<IMapper>();
        mapper.Map<CompanyImportDto>(Arg.Is<EmployeeRawImportDto>(e => e.EmployeeNumber == "Foo")).Returns(companyZuluImportDto);
        mapper.Map<DepartmentImportDto>(Arg.Is<EmployeeRawImportDto>(e => e.EmployeeNumber == "Foo")).Returns(departmentAccountingImportDto);
        mapper.Map<EmployeeImportDto>(Arg.Is<EmployeeRawImportDto>(e => e.EmployeeNumber == "Foo")).Returns(employeeFooImportDto);
        mapper.Map<CompanyImportDto>(Arg.Is<EmployeeRawImportDto>(e => e.EmployeeNumber == "Bar")).Returns(companyDeltaImportDto);
        mapper.Map<DepartmentImportDto>(Arg.Is<EmployeeRawImportDto>(e => e.EmployeeNumber == "Bar")).Returns(departmentEngineeringImportDto);
        mapper.Map<EmployeeImportDto>(Arg.Is<EmployeeRawImportDto>(e => e.EmployeeNumber == "Bar")).Returns(employeeBarImportDto);
        var employeeMapperService = new EmployeeMapperService(mapper);
        List<EmployeeRawImportDto> records = [employeeFooRawImportDto, employeeBarRawImportDto];

        // Act and assert
        var exception = Assert.Throws<InvalidOperationException>(() => employeeMapperService.Map(records, CancellationToken.None));
        Assert.Equal("Manager with number Bar does not exist in company Zulu.", exception.Message);
    }
}
