using Teppi.Application.Mappers;

namespace Teppi.WebApi.Extensions;

public static class ExceptionHandlerExtensions
{
    public static IServiceCollection AddExceptionHandling(this IServiceCollection services)
    {
        services.AddProblemDetails();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        
        services.AddAutoMapper(typeof(UserProfile));
        
        return services;
    }
}
