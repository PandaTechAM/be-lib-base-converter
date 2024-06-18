using BaseConverter.Exceptions;
using Microsoft.AspNetCore.Builder;

namespace BaseConverter.Extensions;

public static class WebApplicationBuilderExtensions
{
    private const string RequiredChars = "0123456789abcdefghijklmnopqrstuvwxyz";

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

        if (!base36Chars.All(RequiredChars.Contains))
            throw new InputValidationException("Base36Chars must contain all digits and lowercase letters from a to z");
    }
}