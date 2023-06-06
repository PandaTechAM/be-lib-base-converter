using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BaseConverter;

public class PandaJsonBaseConverter: JsonConverter<long>, IParameterModelConvention
{
    public override long Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options) =>
        PandaBaseConverter.Base36ToBase10(reader.GetString() ?? "");

    public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(PandaBaseConverter.Base10ToBase36(value));
    }

    public void Apply(ParameterModel parameter)
    {
        if (parameter.ParameterType != typeof(long)) throw new Exception("Parameter type must be long");
        parameter.BindingInfo ??= new BindingInfo();
        parameter.BindingInfo.BinderType = typeof(StringToLongModelBinder);
    }
    
    private class StringToLongModelBinder: IModelBinder
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
}
