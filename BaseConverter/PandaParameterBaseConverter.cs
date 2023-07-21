using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BaseConverter;

[AttributeUsage(AttributeTargets.Parameter)]
public class PandaParameterBaseConverter : Attribute, IParameterModelConvention, IParameterFilter
{
    
    public void Apply(ParameterModel parameter)
    {
        if (parameter.ParameterType != typeof(long) && parameter.ParameterType != typeof(long?))
            throw new Exception("Parameter type must be long or long?");
        parameter.BindingInfo ??= new BindingInfo();
        parameter.BindingInfo.BinderType = typeof(StringToLongModelBinder);
        
    }

    public class StringToLongModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

            var valueAsString = valueProviderResult.FirstValue;
            if (string.IsNullOrEmpty(valueAsString))
            {
                return Task.CompletedTask;
            }

            var result = PandaBaseConverter.Base36ToBase10(valueAsString);
            bindingContext.Result = ModelBindingResult.Success(result);
            return Task.CompletedTask;
        }
    }

    public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
    {
        if (context.ParameterInfo.CustomAttributes.Any(x => x.AttributeType == typeof(PandaParameterBaseConverter)))
        {
            parameter.Schema.Type = "string";
            parameter.Schema.Format = "[0-9a-z]+";
        }
    }
}