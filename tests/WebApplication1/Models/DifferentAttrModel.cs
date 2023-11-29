using System.Text.Json.Serialization;
using BaseConverter;

namespace WebApplication1.Models;

public class DifferentAttrModel
{
    [JsonConverter(typeof(PandaJsonBaseConverterNullable), nameof(NotNullProperty))]
    public long NotNullProperty { get; set; }

    [JsonConverter(typeof(PandaJsonBaseConverterNullable), nameof(NullableProperty))]
    public long? NullableProperty { get; set; }
}