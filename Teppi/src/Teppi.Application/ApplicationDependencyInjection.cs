using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Teppi.Application.Interfaces;
using Teppi.Application.Services;

namespace Teppi.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services,
        IConfiguration configuration // Add this parameter
    )
    {
        services.AddHttpContextAccessor();
        services.RegisterServices();
        services.RegisterRedisCache(configuration);
        return services;
    }

    private static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<IUserService, UserService>();
    }
    
    private static void RegisterRedisCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            string conn = configuration.GetConnectionString("RedisCache") ?? "";
            options.Configuration = conn;

            options.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions()
            {
                AbortOnConnectFail = true,
                EndPoints = { options.Configuration }
            };
        });
    }
} 