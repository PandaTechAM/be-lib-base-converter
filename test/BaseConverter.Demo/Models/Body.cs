using BaseConverter.Attributes;

namespace BaseConverter.Demo.Models;

public class Body
{
    [PropertyBaseConverter] public long NotNullProperty { get; set; }
    [PropertyBaseConverter] public long? NullableProperty { get; set; }
    [PropertyBaseConverter] public List<long> NotNullListProperty { get; set; }
    [PropertyBaseConverter] public List<long?> NullableListProperty { get; set; }
}

public class BodyResponse
{
    public long NotNullProperty { get; set; }
    public long? NullableProperty { get; set; }
    public List<long> NotNullListProperty { get; set; }
    public List<long?> NullableListProperty { get; set; }

    public static BodyResponse FromTestConverterModel(Body @class)
    {
        return new BodyResponse
        {
            NotNullProperty = @class.NotNullProperty,
            NullableProperty = @class.NullableProperty,
            NotNullListProperty = @class.NotNullListProperty,
            NullableListProperty = @class.NullableListProperty
        };
    }
}