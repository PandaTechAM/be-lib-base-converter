using BaseConverter;
using System.Text.Json;
using System.Text.Json.Serialization;

public class MyDataModel
{
    [PandaPropertyBaseConverter]
    public long MyLongValue { get; set; }

    [PandaPropertyBaseConverter]
    public long? MyNullableLongValue { get; set; }
}

internal abstract class PandaJsonBaseConverter<T> : JsonConverter<T>
{
    protected abstract T ReadValue(ref Utf8JsonReader reade);
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return ReadValue(ref reader);
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        if (value == null)
        {
            throw new JsonException("This converter should only be used for non-nullable value types.");
        }

        writer.WriteStringValue(PandaBaseConverter.Base10ToBase36(Convert.ToInt64(value)));
    }

    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert == typeof(T);
    }
}

internal class PandaJsonNotNullableBaseConverter : PandaJsonBaseConverter<long>
{
    protected override long ReadValue(ref Utf8JsonReader reader)
    {
        if (reader.TokenType == JsonTokenType.Number /*&& reader.TryGetInt64(out long value)*/)
        {
            return PandaBaseConverter.Base36ToBase10(reader.GetString()) ??
                throw new JsonException($"Null data for type {typeof(long)}");
        }

        throw new JsonException($"Invalid JSON data type for {typeof(long)}");
    }
}

internal class PandaJsonNullableBaseConverter : PandaJsonBaseConverter<long?>
{
    protected override long? ReadValue(ref Utf8JsonReader reader)
    {
        //if (reader.TokenType == JsonTokenType.Null)
        //{
        //    throw new JsonException("Filed must not be null.");
        //}

        if (reader.TokenType == JsonTokenType.Null || reader.TokenType == JsonTokenType.Number /*&& reader.TryGetInt64(out long value)*/)
        {
            return PandaBaseConverter.Base36ToBase10(reader.GetString());
        }

        throw new JsonException($"Invalid JSON data type for {typeof(long?)}");
    }
}

[AttributeUsage(AttributeTargets.Property)]
public class PandaPropertyBaseConverterAttribute : JsonConverterAttribute
{
    public override JsonConverter CreateConverter(Type typeToConvert)
    {
        var converterType = typeof(PandaJsonBaseConverter<>).MakeGenericType(typeToConvert);
        return (JsonConverter)Activator.CreateInstance(converterType)!;
    }
}
