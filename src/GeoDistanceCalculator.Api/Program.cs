using FluentValidation;
using GeoDistanceCalculator.Application.Commands;
using GeoDistanceCalculator.Application.Interfaces;
using GeoDistanceCalculator.Application.Services;
using GeoDistanceCalculator.Application.Validators;
using GeoDistanceCalculator.Data;
using GeoDistanceCalculator.Data.Interfaces;
using GeoDistanceCalculator.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

const string Title = "Geo Distance Calculator API";
const string Description = "Geo Distance Calculator API<br/><br>This API provides an endpoint to calculate the distance between two coordinates.";
const string Version = "v1";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<DataContext>(options
    => options.UseSqlite(GetConnectionString(), b
        => b.MigrationsAssembly(typeof(DataContext).Assembly.FullName))
    );

builder.Services.AddMediatR(cfg =>
     cfg.RegisterServicesFromAssembly(typeof(CalculateDistanceCommand).Assembly));
builder.Services.AddTransient<IGeoDistanceService, GeoDistanceService>();
builder.Services.AddTransient<IRepository, Respository>();
// Uncomment the line below and comment out the line above to use mock repository rather than real repository which utilises SqlIte and EF Core
//builder.Services.AddTransient<IRepository, MockRepository>();
builder.Services.AddValidatorsFromAssemblyContaining<CalculateDistanceRequestDtoValidator>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(Version, new OpenApiInfo { Title = Title, Version = Version, Description = Description });
    foreach (var name in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.xml", SearchOption.AllDirectories))
    {
        c.IncludeXmlComments(name);
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


static string GetConnectionString()
{
    var directory = AppDomain.CurrentDomain.BaseDirectory;
    var src = directory.Split(new string[] { "src" }, StringSplitOptions.None);
    var formattedString = src[0].Replace("\\", "\\\\");

    return $"Data Source={formattedString}\\src\\CoordinateCalculation.db";
}

public partial class Program 
{ 
    // This is so that the integration tests function correctly with WebApplicaitonFactory<Program>
}
