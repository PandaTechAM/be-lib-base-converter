using BaseConverter.Attributes;
using BaseConverter.Filters;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BaseConverter.Extensions;

public static class SwaggerFilterExtension
{
    public static void AddBaseConverterFilters(this SwaggerGenOptions options)
    {
        options.OperationFilter<MinimalApiFilter>();
        options.ParameterFilter<ParameterBaseConverter>();
        options.SchemaFilter<PropertyFilter>();
    } 
}