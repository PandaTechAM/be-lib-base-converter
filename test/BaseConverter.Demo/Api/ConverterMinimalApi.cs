using BaseConverter.Attributes;
using BaseConverter.Demo.Models;
using BaseConverter.Extensions;
using FluentMinimalApiMapper;
using Microsoft.AspNetCore.Mvc;

namespace BaseConverter.Demo.Api;

public class ConverterMinimalApi : IEndpoint
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var groupApp = app.MapGroup("minimal");


        groupApp.MapGet("query", ([FromQuery] long id) => id)
            .QueryBaseConverter();

        groupApp.MapGet("query-nullable", ([FromQuery] long? id, [FromQuery] long vazgen) => id)
            .QueryBaseConverter();

        groupApp.MapGet("path/{id}", ([FromRoute] long id) => id)
            .RouteBaseConverter();


        groupApp.MapGet("path-nullable/{id}", (long? id) => id)
            .RouteBaseConverter();


        groupApp.MapPost("body", ([FromBody] Body request) => BodyResponse.FromTestConverterModel(request));

        groupApp.MapPost("parameter", ([AsParameters] Parameter request) => request)
            .QueryBaseConverter("Prop1", "Prop2");
    }
}