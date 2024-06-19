using BaseConverter.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace BaseConverter.Extensions;

public static class MinimalApiExtensions
{
    public static RouteHandlerBuilder QueryBaseConverter(this RouteHandlerBuilder builder, string queryParamName = "id")
    {
        builder.WithMetadata(new BaseConverterMetadata("query", queryParamName));

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
    
    public static RouteHandlerBuilder QueryBaseConverter(this RouteHandlerBuilder builder, params string[] queryParamNames)
    {
        foreach (var queryParamName in queryParamNames)
        {
            builder.QueryBaseConverter(queryParamName);
        }

        return builder;
    }

    public static RouteHandlerBuilder RouteBaseConverter(this RouteHandlerBuilder builder, string routeName = "id")
    {
        builder.WithMetadata(new BaseConverterMetadata("route", routeName));

        builder.Add(endpointBuilder =>
        {
            var original = endpointBuilder.RequestDelegate;
            endpointBuilder.RequestDelegate = async context =>
            {
                var routeValues = context.Request.RouteValues;

                if (routeValues.TryGetValue(routeName, out var routeValue))
                {
                    var originalValue = routeValue?.ToString();
                    if (!string.IsNullOrEmpty(originalValue))
                        try
                        {
                            var convertedValue = PandaBaseConverter.Base36ToBase10(originalValue).ToString();
                            routeValues[routeName] = convertedValue;
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
    
    public static RouteHandlerBuilder RouteBaseConverter(this RouteHandlerBuilder builder, params string[] routeNames)
    {
        foreach (var routeName in routeNames)
        {
            builder.RouteBaseConverter(routeName);
        }

        return builder;
    }
}