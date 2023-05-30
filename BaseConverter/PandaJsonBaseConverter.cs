using System.Text.Json;
using System.Text.Json.Serialization;

namespace BaseConverter;

public class PandaJsonBaseConverter: JsonConverter<long>
{
    private readonly PandaBaseConverter _baseConverter = new();
    
    public override long Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options) =>
        _baseConverter.Base36ToBase10(reader.GetString() ?? "");

    public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(_baseConverter.Base10ToBase36(value));
    }
}