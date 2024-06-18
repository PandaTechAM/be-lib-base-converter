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
            .QueryBaseConverter("id");

        groupApp.MapGet("query-nullable", ([FromQuery] long? id) => id)
            .QueryBaseConverter("id");

        groupApp.MapGet("path/{id}", (long id) => id)
            .PathBaseConverter("id");

        groupApp.MapGet("path-nullable/{id}", (long? id) => id)
            .PathBaseConverter("id");

        groupApp.MapPost("body", ([FromBody] Body request) => BodyResponse.FromTestConverterModel(request));
    }
}