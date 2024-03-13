
using System.Text.Json;

namespace BaseConverter.Tests;

public class PandaJsonTests
{
    [Fact]
    public void PandaJsonBaseConverter_DeserializesBase36String_ToLong()
    {
        var options = new JsonSerializerOptions();
        options.Converters.Add(new PandaJsonBaseConverter<long>());
        string json = "\"i6\""; // Represents the base36 encoded value for 654

        var result = JsonSerializer.Deserialize<long>(json, options);

        Assert.Equal(654, result);
    }
    
    [Fact]
    public void PandaJsonBaseConverter_SerializesLong_ToBase36String()
    {
        var options = new JsonSerializerOptions();
        options.Converters.Add(new PandaJsonBaseConverter<long>());
        long value = 654;

        var json = JsonSerializer.Serialize(value, options);

        Assert.Equal("\"i6\"", json); // "i6" is the base36 representation of 654
    }


}