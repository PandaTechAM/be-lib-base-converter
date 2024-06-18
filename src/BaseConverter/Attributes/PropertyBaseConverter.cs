using System.Collections;
using System.Text.Json.Serialization;
using BaseConverter.Converters;

namespace BaseConverter.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class PropertyBaseConverter : JsonConverterAttribute
{
    public override JsonConverter CreateConverter(Type typeToConvert)
    {
        if (typeToConvert.IsGenericType && typeof(IEnumerable).IsAssignableFrom(typeToConvert))
        {
            var listElementType = typeToConvert.GetGenericArguments()[0];
            var listConverterType = typeof(JsonBaseConverterList<>).MakeGenericType(listElementType);
            return (JsonConverter)Activator.CreateInstance(listConverterType)!;
        }

        var singleConverterType = typeof(JsonBaseConverter<>).MakeGenericType(typeToConvert);
        return (JsonConverter)Activator.CreateInstance(singleConverterType)!;
    }
}