using System.Text.Json;
using BaseConverter.Attributes;
using BaseConverter.Converters;

namespace BaseConverter.Tests;

public class PandaPropertyConverter
{
    [Fact]
    public void PandaPropertyBaseConverterAttribute_SerializesProperty_ToBase36String()
    {
        var model = new TestModel { TestValue = 654 };
        var options = new JsonSerializerOptions();
        options.Converters.Add(new JsonBaseConverter<long>());

        var json = JsonSerializer.Serialize(model, options);

        Assert.Contains("\"TestValue\":\"i6\"", json); // Check if the property is serialized to base36 string
    }

    [Fact]
    public void PandaPropertyBaseConverterAttribute_DeserializesProperty_FromBase36String()
    {
        var json = "{\"TestValue\":\"i6\"}"; // "i6" is the base36 representation of 654
        var options = new JsonSerializerOptions();
        options.Converters.Add(new JsonBaseConverter<long>());

        var model = JsonSerializer.Deserialize<TestModel>(json, options);

        Assert.Equal(654, model?.TestValue); // Check if the property is deserialized from base36 string to long
    }

    public class TestModel
    {
        [PropertyBaseConverter] public long TestValue { get; set; }
    }
}