using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BaseConverter;

public class PandaJsonBaseConverterNullable : JsonConverter<long?>
{
    private string PropertyName { get; set; }

    public PandaJsonBaseConverterNullable([CallerMemberName] string caller = "")
    {
        PropertyName = caller;
    }

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
    private string PropertyName { get; set; }

    public PandaJsonBaseConverterNotNullable([CallerMemberName] string caller = "")
    {
        PropertyName = caller;
    }

    public override long Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        if (string.IsNullOrWhiteSpace(reader.GetString()))
            throw new ArgumentException($"Null value is not allowed for property {PropertyName}({typeToConvert.Name})");

        if (string.IsNullOrEmpty(reader.GetString()))
            throw new ArgumentException(
                $"Empty value is not allowed for property {PropertyName}({typeToConvert.Name})");

        if (reader.GetString()!.Contains('-'))
            throw new ArgumentException(
                $"Value can't be less than 0 for property {PropertyName}({typeToConvert.Name})");

        return PandaBaseConverter.Base36ToBase10(reader.GetString())!.Value;
    }

    public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(PandaBaseConverter.Base10ToBase36(value));
    }
}