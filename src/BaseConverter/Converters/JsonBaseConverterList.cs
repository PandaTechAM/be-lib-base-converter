using System.Text.Json;
using System.Text.Json.Serialization;

namespace BaseConverter.Converters;

internal class JsonBaseConverterList<T> : JsonConverter<List<T>>
{
    public override List<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
            throw new JsonException($"Expected start of array, got {reader.TokenType}.");

        var list = new List<T>();
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndArray) return list;

            if (reader.TokenType == JsonTokenType.Null)
            {
                list.Add(default!);
            }
            else
            {
                var base36String = reader.GetString();
                if (base36String == null)
                {
                    list.Add(default!);
                }
                else
                {
                    var base10Value = (T)Convert.ChangeType(PandaBaseConverter.Base36ToBase10(base36String),
                        Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T))!;
                    list.Add(base10Value);
                }
            }
        }

        throw new JsonException("Expected end of array.");
    }

    public override void Write(Utf8JsonWriter writer, List<T> value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        foreach (var item in value)
            if (item == null)
            {
                writer.WriteNullValue();
            }
            else
            {
                var base36String = PandaBaseConverter.Base10ToBase36(Convert.ToInt64(item));
                writer.WriteStringValue(base36String);
            }

        writer.WriteEndArray();
    }
}