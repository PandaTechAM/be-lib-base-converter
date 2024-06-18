using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BaseConverter.Filters;

public class PropertyBaseConverterFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(long) || context.Type == typeof(long?))
        {
            schema.Type = "string";
            schema.Format = "[0-9a-z]+";
        }
        else if (context.Type.IsGenericType && context.Type.GetGenericTypeDefinition() == typeof(List<>))
        {
            var itemType = context.Type.GetGenericArguments()[0];
            if (itemType == typeof(long) || itemType == typeof(long?))
            {
                schema.Type = "array";
                schema.Items = new OpenApiSchema { Type = "string", Format = "[0-9a-z]+" };
            }
        }
    }
}