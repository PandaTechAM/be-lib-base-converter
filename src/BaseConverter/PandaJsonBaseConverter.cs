using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;
using BaseConverter.Exceptions;

namespace BaseConverter;

internal class PandaJsonBaseConverter<T> : JsonConverter<T>
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
            throw new InputValidationException($"Wrong value for property ({typeToConvert.Name})");

        var value = reader.GetString();

        if (typeToConvert == typeof(long))
        {
            if (value?.Trim() == string.Empty)
                throw new InputValidationException($"Null/Empty value is not allowed for property ({typeToConvert.Name})");
        }

        if (value!.Contains('-'))
            throw new InputValidationException($"The value can't be less than 1 for property ({typeToConvert.Name})");

        var method = typeof(PandaBaseConverter).GetMethod("Base36ToBase10");

        var call = Expression.Call(null, method!, Expression.Constant(value));

        var lambda = Expression.Lambda<Func<T>>(
            Expression.Convert(call, typeof(T)));
        return lambda.Compile().Invoke();
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

