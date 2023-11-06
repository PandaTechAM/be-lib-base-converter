using BaseConverter;
using Newtonsoft.Json;

namespace WebApplication1.Models;

public class DifferentAttrModel
{
    [JsonConverter(typeof(PandaJsonBaseConverterNullable), nameof(NotNullProperty))]
    public long NotNullProperty { get; set; }

    [JsonConverter(typeof(PandaJsonBaseConverterNullable), nameof(NullableProperty))]
    public long? NullableProperty { get; set; }
}