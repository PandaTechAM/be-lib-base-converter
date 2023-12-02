using Microsoft.AspNetCore.Builder;

namespace BaseConverter;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder ConfigureBaseConverter(this WebApplicationBuilder builder,
        string base36Chars)
    {
        ValidateBase36Chars(base36Chars);

        PandaBaseConverter.Base36Chars = base36Chars;
        return builder;
    }

    private static void ValidateBase36Chars(string base36Chars)
    {
        if (base36Chars.Length != 36 && base36Chars.Distinct().Count() != 36)
        {
            throw new ArgumentException("Base36Chars must be 36 characters long");
        }

        if (base36Chars != base36Chars.ToLower())
        {
            throw new ArgumentException("All characters in Base36Chars must be lowercase");
        }

        const string requiredChars = "0123456789abcdefghijklmnopqrstuvwxyz";

        if (!base36Chars.All(requiredChars.Contains))
        {
            throw new ArgumentException("Base36Chars must contain all digits and lowercase letters from a to z");
        }
    }
}