using CompanyVault.WebApi.Formatters;
using CompanyVault.WebApi.Middlewares;
using CompanyVault.WebApi.Repositories;
using CompanyVault.WebApi.Repositories.Abstractions;
using CompanyVault.WebApi.Repositories.Implementations;
using CompanyVault.WebApi.Services.Abstractions;
using CompanyVault.WebApi.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

const string AppSettingsSectionDatabaseName = "Database:Name";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.InputFormatters.Add(new CsvInputFormatter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<ICsvParserService, CsvParserService>();
builder.Services.AddScoped<IEmployeeMapperService, EmployeeMapperService>();
builder.Services.AddDbContext<CompanyVaultDbContext>(options =>
    {
        options.UseSqlite($"Data Source={builder.Configuration.GetSection(AppSettingsSectionDatabaseName).Get<string>()}");
    });
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Create the database and schema.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CompanyVaultDbContext>();
    if (app.Environment.EnvironmentName == "Test")
    {
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
    }
    else
    {
        db.Database.Migrate();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.MapControllers();
app.Run();

// This declaration is required for integration tests.
public partial class Program { }
