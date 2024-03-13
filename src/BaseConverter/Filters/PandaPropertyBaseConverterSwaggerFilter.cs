using System.Reflection;
using System.Text.Json.Serialization;
using BaseConverter.Attributes;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BaseConverter.Filters;

// ReSharper disable once ClassNeverInstantiated.Global
public class PandaPropertyBaseConverterSwaggerFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        // Loop through the properties of the class
        foreach (var propertyInfo in context.Type.GetProperties())
        {
            // Check if the property has the PandaPropertyBaseConverterAttribute
            var hasAttribute = Attribute.IsDefined(propertyInfo, typeof(PandaPropertyBaseConverterAttribute));
            if (!hasAttribute) continue;

            // The key in the schema's properties dictionary might be different due to naming strategies (e.g., camelCase)
            var jsonPropertyName = propertyInfo.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name ??
                                   propertyInfo.Name;
            var schemaPropertyName =
                char.ToLowerInvariant(jsonPropertyName[0]) +
                jsonPropertyName[1..]; // Convert to camelCase if necessary

            if (schema.Properties.TryGetValue(schemaPropertyName, out var propertySchema))
            {
                propertySchema.Type = "string";
                propertySchema.Format = "base36-encoded";
            }
        }
    }
}