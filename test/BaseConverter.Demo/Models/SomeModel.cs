using BaseConverter.Attributes;

namespace BaseConverter.Demo.Models
{
    public class SomeModel
    {
        [PandaPropertyBaseConverter] public long NotNullProperty { get; set; }
        [PandaPropertyBaseConverter] public long? NullableProperty { get; set; }
    }
}