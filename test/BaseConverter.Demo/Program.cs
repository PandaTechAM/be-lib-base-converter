using BaseConverter.Attributes;
using BaseConverter.Filters;
using FluentMinimalApiMapper;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.AddEndpoints();

builder.Services.AddSwaggerGen(
    options =>
    {
        options.ParameterFilter<ParameterBaseConverter>();
        options.SchemaFilter<PropertyBaseConverterFilter>();
    }
);
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.MapEndpoints();
app.Run();

namespace BaseConverter.Demo
{
    public class Program;
}