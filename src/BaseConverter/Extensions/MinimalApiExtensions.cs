using BaseConverter.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace BaseConverter.Extensions;

public static class MinimalApiExtensions
{
    public static RouteHandlerBuilder QueryBaseConverter(this RouteHandlerBuilder builder, string queryParamName)
    {
        builder.Add(endpointBuilder =>
        {
            var original = endpointBuilder.RequestDelegate;
            endpointBuilder.RequestDelegate = async context =>
            {
                if (context.Request.Query.TryGetValue(queryParamName, out var queryValues))
                {
                    var originalValue = queryValues.FirstOrDefault();
                    if (!string.IsNullOrEmpty(originalValue))
                        try
                        {
                            var convertedValue = PandaBaseConverter.Base36ToBase10(originalValue).ToString();

                            var modifiedQuery = context.Request.Query.ToDictionary(kv => kv.Key, kv => kv.Value);
                            modifiedQuery[queryParamName] = new StringValues(convertedValue);

                            context.Request.Query = new QueryCollection(modifiedQuery);
                        }
                        catch (Exception ex)
                        {
                            throw new BaseConverterException(ex.Message, originalValue);
                        }
                }

                await original!(context);
            };
        });
        return builder;
    }

    public static RouteHandlerBuilder PathBaseConverter(this RouteHandlerBuilder builder, string pathParamName)
    {
        builder.Add(endpointBuilder =>
        {
            var original = endpointBuilder.RequestDelegate;
            endpointBuilder.RequestDelegate = async context =>
            {
                var routeValues = context.Request.RouteValues;

                if (routeValues.TryGetValue(pathParamName, out var pathValue))
                {
                    var originalValue = pathValue?.ToString();
                    if (!string.IsNullOrEmpty(originalValue))
                        try
                        {
                            var convertedValue = PandaBaseConverter.Base36ToBase10(originalValue).ToString();
                            routeValues[pathParamName] = convertedValue;
                        }
                        catch (Exception ex)
                        {
                            throw new BaseConverterException(ex.Message, originalValue);
                        }
                }

                await original!(context);
            };
        });
        return builder;
    }
}