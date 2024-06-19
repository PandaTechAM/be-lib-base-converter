using BaseConverter.Extensions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BaseConverter.Filters;

public class MinimalApiFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var endpointMetadata = context.ApiDescription.ActionDescriptor.EndpointMetadata
            .OfType<BaseConverterMetadata>();

        foreach (var metadata in endpointMetadata)
        {
            if (operation.Parameters == null) continue;

            foreach (var parameter in operation.Parameters)
            {
                if (parameter.Name == metadata.ParameterName)
                {
                    if (metadata.ConverterType == "query" && 
                        parameter.In == ParameterLocation.Query)
                    {
                        // Modify the schema for query parameters
                        parameter.Schema.Type = "string";
                        parameter.Schema.Format = "base36-encoded";
                    }
                    else if (metadata.ConverterType == "route" &&
                             parameter.In == ParameterLocation.Path)
                    {
                        // Modify the schema for route parameters
                        parameter.Schema.Type = "string";
                        parameter.Schema.Format = "base36-encoded";
                    }
                }
            }
        }
    }
}