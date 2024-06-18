namespace BaseConverter.Exceptions;

public class BaseConverterException(string? message, string? value = null) : Exception(message)
{
    private readonly string? _message = message;
    private string? Value { get; } = value;
    public string? FullMessage => $"Message: {_message} with Value: {Value}";
}