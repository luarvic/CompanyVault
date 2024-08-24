# CompanyVault

![Dev build status](https://github.com/luarvic/CompanyVault/actions/workflows/build.yml/badge.svg)

A .NET Web API service for storing and managing employee data.

## Table of Contents

1. [CompanyVault specification.](#companyvault-specification)
1. [How to run locally.](#how-to-run-locally)
1. [How to run tests.](#how-to-run-tests)
1. [Architecture and design decisions.](#architecture-and-design-decisions)

## CompanyVault Specification

CompanyVault is a web API microservice for storing and managing employee data.

It allows you to:

- Upload employee data in CSV format (a [sample](./data/testing-data.csv)).
- Retrieve employee and company data with the endpoints defined in the [specification](./docs/requirements.pdf).

## How to Run Locally

### Prerequisites

- You have installed a [Git client](https://git-scm.com/downloads).
- You have installed [.NET](https://dotnet.microsoft.com/en-us/download) version `8.0.x` or later.

To verify, run in terminal:

```bash
git --version
dotnet --version
```

### Getting Started

#### 1. Clone the Repository

Run in terminal:

```bash
git clone git@github.com:luarvic/CompanyVault.git
```

#### 2. Navigate to the Service Source Directory

Run in terminal:

```bash
cd CompanyVault/src/CompanyVault.WebApi
```

#### 3. Run the Service

Run in terminal:

```bash
dotnet run
```

Make sure you see `Application started. Press Ctrl+C to shut down.` in the terminal window.

#### 4. Verify Running Service

Open [http://localhost:5068/swagger](http://localhost:5068/swagger) in web browser.

Make sure you see a page with `Swagger UI` title.

## How to Run Tests

#### 1. Navigate to the Service Source Directory

Run in terminal:

```bash
cd CompanyVault/tests/CompanyVault.WebApi.Tests
```

#### 2. Run the Tests

Run in terminal:

```bash
dotnet test
```

## Architecture and Design Decisions

### Folders Structure

- `data` contains data samples
- `docs` contains requirements
- `src` contains source code
  - `CompanyVault.WebApi` contains the service project
    - `Controllers` contains classes that implement endpoints
    - `Formatters` contains classes that add [custom formatters](https://learn.microsoft.com/en-us/aspnet/core/web-api/advanced/custom-formatters?view=aspnetcore-8.0), e.g. CSV input formatter
    - `Middlewares` contains classes the implement [custom middlewares](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/write?view=aspnetcore-8.0), e.g. exception handler middleware
    - `Migrations` contains classes generated by [Entity Framework migrations](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli)
    - `Models` contains classes that represent domain entities and data transfer objects (DTOs)
    - `Properties` contains launch settings
    - `Repositories` contains classes that implement [Repository and Unit of Work patterns](https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application)
    - `Services` contains classes that implement business logic, e.g. mapping CSV data to DTOs, and DTOs to entities
- `tests` contains unit and integration tests
  - `CompanyVault.WebApi.Tests` contains the test project
    - `IntegrationTests` contains classes that test endpoints by sending HTTP request to the service with mocked infrastructure, i.e. database
    - `UnitTests` contains classes that test the services

### Dependencies

#### `CompanyVault.WebApi` Dependencies

Among other things, `CompanyVault.WebApi` project depends on the following NuGet packages:

- [CsvHelper](https://www.nuget.org/packages/CsvHelper) simplifies CSV data loading.
- [AutoMapper](https://www.nuget.org/packages/automapper/) simplifies objects mapping.
- [Microsoft.EntityFrameworkCore.Sqlite](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Sqlite) enables SQLite database in Entity Framework.

#### `CompanyVault.WebApi.Tests` Dependencies

Among other things, `CompanyVault.WebApi.Tests` project depends on the following NuGet packages:

- [NSubstitute](https://www.nuget.org/packages/NSubstitute/) mocks objects for testing.
- [xunit](https://www.nuget.org/packages/xunit) is a unit testing framework.
