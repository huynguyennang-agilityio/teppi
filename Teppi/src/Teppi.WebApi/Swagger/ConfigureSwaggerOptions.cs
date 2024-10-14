using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Teppi.WebApi.Swagger;

public class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) : IConfigureOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
        }
    }

    /// <summary>
    /// Creates an OpenApiInfo object for a given API version.
    /// </summary>
    /// <param name="description">The description of the API version.</param>
    /// <returns>An OpenApiInfo
    private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        // Create a new OpenApiInfo object with the title, version, description, and contact details.
        var info = new OpenApiInfo()
        {
            Title = "Teppi API Documentation",
            Version = description.ApiVersion.ToString(),
            Description = $"Teppi Web API {description.ApiVersion} for managing the shopping store.",
            Contact = new OpenApiContact() { Name = "Support", Email = "support@asnet.com.vn" }
        };

        if (description.IsDeprecated)
        {
            info.Description += " This API version has been deprecated.";
        }

        return info;
    }
}
