using System.Security.Claims;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Teppi.Data.Extensions;
using Teppi.Data.Persistence;
using Teppi.WebApi;
using Teppi.WebApi.Extensions;
using Teppi.WebApi.Utils;

var builder = WebApplication.CreateBuilder(args);

// AddDbContextConfig is an extension method that configures the database context for the application.
builder.Services.AddDbContextConfig(builder.Configuration);


// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

// Configuring Dependency Injection
builder.Services.AddApplication(builder.Configuration);

// Configuring API versioning
builder.Services.AddApiVersionOptions();

builder.Services.AddExceptionHandling();

builder.Services.AddAuthorization();

builder.Services
    .AddControllers(options => options.Filters.Add(typeof(ValidateModelAttribute)))
    .AddJsonOptions(options => options.JsonSerializerOptions
        .Converters.Add(new JsonStringEnumConverter()));

var app = builder.Build();
app.CreateDbIfNotExists();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var descriptions = app.DescribeApiVersions();

        // Build a Swagger endpoint for each discovered API version
        foreach (var description in descriptions)
        {
            var url = $"/swagger/{description.GroupName}/swagger.json";
            var name = description.GroupName.ToUpperInvariant();
            options.SwaggerEndpoint(url, name);
        }
    });
    app.UseDeveloperExceptionPage();
    using var scope = app.Services.CreateScope();

    // Seed DB
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
    AppDbContextSeed.RunMigrationAsync(userManager, roleManager).Wait();

}

app.UseHttpsRedirection();
app.MapControllers();

app.UseExceptionHandler("/error");

app.MapGroup("api/v1/Auth").MapIdentityApi<User>();

app.Run();

// Make the implicit Program class public so test projects can access it
public partial class Program
{
}