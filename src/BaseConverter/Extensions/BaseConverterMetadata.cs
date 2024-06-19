namespace BaseConverter.Extensions;

public class BaseConverterMetadata
{
    public string ConverterType { get; }
    public string ParameterName { get; }

    public BaseConverterMetadata(string converterType, string parameterName)
    {
        ConverterType = converterType;
        ParameterName = parameterName;
    }
}