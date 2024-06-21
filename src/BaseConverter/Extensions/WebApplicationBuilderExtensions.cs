using BaseConverter.Exceptions;
using Microsoft.AspNetCore.Builder;

namespace BaseConverter.Extensions;

public static class WebApplicationBuilderExtensions
{
    private const string Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public static WebApplicationBuilder ConfigureBaseConverter(this WebApplicationBuilder builder,
        string base36Chars)
    {
        ValidateBase36Chars(base36Chars);

        PandaBaseConverter.Base36Chars = base36Chars;
        return builder;
    }

    private static void ValidateBase36Chars(string base36Chars)
    {
        if (base36Chars.Length != 36 || base36Chars.Distinct().Count() != 36)
            throw new InputValidationException("Base36Chars must be 36 characters long");

        if (!base36Chars.ToUpper().All(Chars.ToUpper().Contains))
            throw new InputValidationException("Base36Chars must contain all digits and letters from a to z");
    }
}