using System.ComponentModel;
using System.Text.Json.Serialization;
using BaseConverter;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Test;

[Controller]
public class Controller
{
    [HttpGet("Test/{testEnum}")]
    public string Test([EndpointStringToLongConverter]  long testEnum)
    {
        return testEnum.ToString();
    }
    


    
    

}

class SomeModel
{
    [JsonConverter(typeof(PandaJsonBaseConverter))]
    public long Id { get; set; } 
}

public class EndpointStringToLongConverter: Attribute, IParameterModelConvention
{
    public void Apply(ParameterModel parameter)
    {
        if (parameter.ParameterType == typeof(long))
        {
            parameter.BindingInfo ??= new BindingInfo();
            parameter.BindingInfo.BinderType = typeof(StringToLongModelBinder);
        }
        
    }
}

public class StringToLongModelBinder: IModelBinder
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

