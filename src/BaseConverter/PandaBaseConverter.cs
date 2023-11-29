namespace BaseConverter;

public static class PandaBaseConverter
{
    private static readonly string Base36Chars =
        Environment.GetEnvironmentVariable("BASE36_CHARS") ?? "0123456789abcdefghijklmnopqrstuvwxyz";

    public static string? Base10ToBase36(long? base10Number)
    {
        if (base10Number == null)
        {
            return null;
        }

        var base36 = "";

        try
        {
            if (base10Number < 0)
            {
                throw new ArgumentException("Base10 only accepts positive numbers");
            }

            if (Base36Chars.Length != 36)
            {
                throw new ArgumentException("BASE36_CHARS must be 36 characters long");
            }

            while (base10Number > 0)
            {
                var remainder = (int)(base10Number % 36);
                base36 = Base36Chars[remainder] + base36;
                base10Number /= 36;
            }

            return base36;
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public static long? Base36ToBase10(string? base36String)
    {
        if (base36String == null)
        {
            return null;
        }

        long base10Value = 0;
        var power = 0;

        try
        {
            if (base36String.Any(c => !Base36Chars.Contains(c)))
            {
                throw new ArgumentException("Base36 only accepts characters 0-9 and a-z");
            }

            if (Base36Chars.Length != 36)
            {
                throw new ArgumentException("BASE36_CHARS must be 36 characters long");
            }

            for (var i = base36String.Length - 1; i >= 0; i--)
            {
                var digitValue = Base36Chars.IndexOf(base36String[i]);
                base10Value += digitValue * (long)Math.Pow(36, power);
                power++;
            }
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }

        return base10Value;
    }
}