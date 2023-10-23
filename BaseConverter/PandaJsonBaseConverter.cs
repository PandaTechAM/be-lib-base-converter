using System.Text.Json;
using System.Text.Json.Serialization;

namespace BaseConverter;

public class PandaJsonBaseConverterNullable : JsonConverter<long?>
{
    public override long? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options) =>
        PandaBaseConverter.Base36ToBase10(reader.GetString());

    public override void Write(Utf8JsonWriter writer, long? value, JsonSerializerOptions options)
    {
        if (value is not null)
            writer.WriteStringValue(PandaBaseConverter.Base10ToBase36(value.Value));
        else
            writer.WriteNullValue();
    }
}

public class PandaJsonBaseConverterNotNullable : JsonConverter<long>
{
    public override long Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        if(reader.GetString() is null)
            throw new ArgumentException($"Null value is not allowed for type {typeToConvert.Name}");

        if (reader.GetString()!.Contains('-'))
            throw new ArgumentException($"Value can't be less than 0 for type {typeToConvert.Name}");

        return PandaBaseConverter.Base36ToBase10(reader.GetString())!.Value;
    }

    public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(PandaBaseConverter.Base10ToBase36(value));
    }
}