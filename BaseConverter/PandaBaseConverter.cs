namespace BaseConverter;

public class PandaBaseConverter
{
    public PandaBaseConverter()
    {
        _base36Chars = Environment.GetEnvironmentVariable("BASE36_CHARS") ?? "0123456789abcdefghijklmnopqrstuvwxyz";
    }

    readonly string _base36Chars;

    public string Base10ToBase36(long base10Number)
    {
        string base36 = "";

        try
        {
            if (base10Number < 0)
            {
                throw new System.ArgumentException("Base10 only accepts positive numbers");
            }

            else if (_base36Chars.Length != 36)
            {
                throw new System.ArgumentException("BASE36_CHARS must be 36 characters long");
            }
            else
            {
                while (base10Number > 0)
                {
                    int remainder = (int)(base10Number % 36);
                    base36 = _base36Chars[remainder] + base36;
                    base10Number /= 36;
                }

                return base36;
            }
        }
        catch (System.ArgumentException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public long Base36ToBase10(string base36String)
    {
        long base10Value = 0;
        int power = 0;

        try
        {
            if (base36String.Any(c => !_base36Chars.Contains(c)))
            {
                throw new System.ArgumentException("Base36 only accepts characters 0-9 and a-z");
            }

            else if (_base36Chars.Length != 36)
            {
                throw new System.ArgumentException("BASE36_CHARS must be 36 characters long");
            }
            else
            {
                for (int i = base36String.Length - 1; i >= 0; i--)
                {
                    int digitValue = _base36Chars.IndexOf(base36String[i]);
                    base10Value += digitValue * (long)Math.Pow(36, power);
                    power++;
                }
            }
        }
        catch (System.ArgumentException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }

        return base10Value;
    }
}