using System.Text.Json;
using System.Text.Json.Serialization;

namespace BaseConverter;

public class PandaJsonBaseConverter: JsonConverter<long>
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
}