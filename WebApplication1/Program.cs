using BaseConverter;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static BaseConverter.PandaParameterBaseConverterAttribute;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
        options.ParameterFilter<PandaParameterBaseConverterAttribute>()
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public class StringToLongModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context.Metadata.ModelType == typeof(long) || context.Metadata.ModelType == typeof(long?))
        {
            return new StringToLongModelBinder();
        }

        return null;
    }
}