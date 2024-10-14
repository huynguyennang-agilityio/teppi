using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Teppi.WebApi.Swagger;

public class CustomHeaderFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= [];

        var apiDescription = context.ApiDescription;
        if (apiDescription.IsDeprecated())
        {
            operation.Deprecated = true;
        }
    }
}
