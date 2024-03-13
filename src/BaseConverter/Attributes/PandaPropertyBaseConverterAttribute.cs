using System.Text.Json.Serialization;

namespace BaseConverter.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class PandaPropertyBaseConverterAttribute : JsonConverterAttribute
{
    public override JsonConverter CreateConverter(Type typeToConvert)
    {
        var converterType = typeof(PandaJsonBaseConverter<>).MakeGenericType(typeToConvert);
        return (JsonConverter)Activator.CreateInstance(converterType)!;
    }
}