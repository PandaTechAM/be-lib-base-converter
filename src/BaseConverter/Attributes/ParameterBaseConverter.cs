using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BaseConverter.Attributes;

[AttributeUsage(AttributeTargets.Parameter)]
public class ParameterBaseConverter : Attribute, IParameterModelConvention, IParameterFilter
{
    public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
    {
        if (context.ParameterInfo.CustomAttributes.All(x =>
                x.AttributeType != typeof(ParameterBaseConverter))) return;

        if (parameter.Schema.Type == "array")
        {
            parameter.Schema.Items = new OpenApiSchema { Type = "string", Format = "base36-encoded" };
        }
        else
        {
            parameter.Schema.Type = "string";
            parameter.Schema.Format = "base36-encoded";
        }
    }

    public void Apply(ParameterModel parameter)
    {
        if (parameter.ParameterType == typeof(long) || parameter.ParameterType == typeof(long?))
        {
            parameter.BindingInfo ??= new BindingInfo();
            parameter.BindingInfo.BinderType = typeof(StringToLongModelBinder);
        }
        else if (parameter.ParameterType == typeof(List<long>) || parameter.ParameterType == typeof(List<long?>))
        {
            parameter.BindingInfo ??= new BindingInfo();
            parameter.BindingInfo.BinderType = typeof(StringToLongListModelBinder);
        }
        else
        {
            throw new Exception("Parameter type must be long, long?, List<long> or List<long?>");
        }
    }

    private class StringToLongModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProviderResult == ValueProviderResult.None) return Task.CompletedTask;

            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

            var valueAsString = valueProviderResult.FirstValue;
            if (string.IsNullOrEmpty(valueAsString)) return Task.CompletedTask;

            var result = PandaBaseConverter.Base36ToBase10(valueAsString);
            bindingContext.Result = ModelBindingResult.Success(result);
            return Task.CompletedTask;
        }
    }

    private class StringToLongListModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProviderResult == ValueProviderResult.None) return Task.CompletedTask;

            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

            var valueAsString = valueProviderResult.FirstValue;
            if (string.IsNullOrEmpty(valueAsString)) return Task.CompletedTask;

            var base36List = valueAsString.Split(',').ToList();
            var result = PandaBaseConverter.Base36ListToBase10List(base36List);
            bindingContext.Result = ModelBindingResult.Success(result);
            return Task.CompletedTask;
        }
    }
}