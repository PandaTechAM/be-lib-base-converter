using System.Text.Json;
using System.Text.Json.Serialization;

namespace BaseConverter.Converters;

internal class JsonBaseConverter<T> : JsonConverter<T>
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            if (Nullable.GetUnderlyingType(typeToConvert) == null)
                throw new JsonException($"Cannot convert null to {typeToConvert}");
            return default!;
        }

        if (reader.TokenType != JsonTokenType.String)
            throw new JsonException(
                $"Unexpected token parsing {typeToConvert}. Expected String, got {reader.TokenType}.");

        var value = reader.GetString();
        if (string.IsNullOrEmpty(value)) return default!;

        var base10Value = (T)Convert.ChangeType(PandaBaseConverter.Base36ToBase10(value),
            Nullable.GetUnderlyingType(typeToConvert) ?? typeToConvert)!;
        return base10Value;
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        if (value == null)
        {
            writer.WriteNullValue();
            return;
        }

        var base36String = PandaBaseConverter.Base10ToBase36(Convert.ToInt64(value));
        writer.WriteStringValue(base36String);
    }
}