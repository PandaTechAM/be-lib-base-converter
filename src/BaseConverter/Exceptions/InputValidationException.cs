namespace BaseConverter.Exceptions;

public class InputValidationException(string? message, string? value = null)
    : BaseConverterException(message, value);
