namespace BaseConverter;

public static class PandaBaseConverter
{
    private static string _base36Chars = Environment.GetEnvironmentVariable("BASE36_CHARS") ?? "0123456789abcdefghijklmnopqrstuvwxyz";;

    public static string Base10ToBase36(long base10Number)
    {
        var base36 = "";

        try
        {
            if (base10Number < 0)
            {
                throw new ArgumentException("Base10 only accepts positive numbers");
            }

            if (_base36Chars.Length != 36)
            {
                throw new ArgumentException("BASE36_CHARS must be 36 characters long");
            }

            while (base10Number > 0)
            {
                var remainder = (int)(base10Number % 36);
                base36 = _base36Chars[remainder] + base36;
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

    public static long Base36ToBase10(string base36String)
    {
        long base10Value = 0;
        var power = 0;

        try
        {
            if (base36String.Any(c => !_base36Chars.Contains(c)))
            {
                throw new ArgumentException("Base36 only accepts characters 0-9 and a-z");
            }

            if (_base36Chars.Length != 36)
            {
                throw new ArgumentException("BASE36_CHARS must be 36 characters long");
            }
            for (var i = base36String.Length - 1; i >= 0; i--)
            {
                var digitValue = _base36Chars.IndexOf(base36String[i]);
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
