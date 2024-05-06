namespace BaseConverter.Exceptions;

public class UnsupportedCharacterException(string? message, string? value = null)
    : BaseConverterException(message, value)
{
    
}