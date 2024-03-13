using System.Text;

namespace BaseConverter;

public static class PandaBaseConverter
{
    public static string Base36Chars { get; set; } = "0123456789abcdefghijklmnopqrstuvwxyz";

    public static string Base10ToBase36(long base10Number)
    {
        if (base10Number < 0)
        {
            throw new ArgumentException("Base10 only accepts positive numbers");
        }

        var builder = new StringBuilder();

        while (base10Number > 0)
        {
            var remainder = (int)(base10Number % 36);
            builder.Insert(0, Base36Chars[remainder]);
            base10Number /= 36;
        }

        return builder.ToString();
    }

    public static string? Base10ToBase36(long? base10Number)
    {
        return base10Number.HasValue ? Base10ToBase36(base10Number.Value) : null;
    }

    public static long? Base36ToBase10(string? base36String = null)
    {
        if (base36String is null)
        {
            return null;
        }

        return Base36ToBase10NotNull(base36String);
    }

    public static long Base36ToBase10NotNull(string base36String)
    {
        if (string.IsNullOrEmpty(base36String))
        {
            throw new ArgumentException("Input string cannot be null or empty.", nameof(base36String));
        }

        if (!ValidateBase36Chars(base36String))
        {
            throw new ArgumentException("Base36 only accepts characters 0-9 and a-z", nameof(base36String));
        }

        long base10Value = 0;
        foreach (var c in base36String)
        {
            base10Value = base10Value * 36 + Base36Chars.IndexOf(c);
        }

        return base10Value;
    }

    public static bool ValidateBase36Chars(this string base36String)
    {
        return base36String.All(c => Base36Chars.Contains(c));
    }
}