using System.ComponentModel.DataAnnotations;

namespace TestConverter;

public class UnitTests
{
    [Fact]
    public void Base10ToBase36_ConvertsPositiveNumber_ReturnsCorrectBase36Representation()
    {
        var converter = new BaseConverter.PandaBaseConverter();
        var result1 = converter.Base10ToBase36(654);
        var result2 = converter.Base10ToBase36(7896314);
        var result3 = converter.Base10ToBase36(123456789);
        var result4 = converter.Base10ToBase36(9874564789631);
        Assert.Equal("i6", result1);
        Assert.Equal("4p8u2", result2);
        Assert.Equal("21i3v9", result3);
        Assert.Equal("3i0b8x6yn", result4);
    }

    [Fact]
    public void Base10ToBase36_ThrowsException_WhenNegativeBase10ValueIsUsed()
    {
        var converter = new BaseConverter.PandaBaseConverter();
        Assert.Throws<ArgumentException>(() => converter.Base10ToBase36(-1));
    }

    [Fact]
    public void Base36ToBase10_ConvertsPositiveBase36Number_ReturnsCorrectBase10Value()
    {
        var converter = new BaseConverter.PandaBaseConverter();
        var result1 = converter.Base36ToBase10("i6");
        var result2 = converter.Base36ToBase10("4p8u2");
        var result3 = converter.Base36ToBase10("21i3v9");
        var result4 = converter.Base36ToBase10("3i0b8x6yn");
        Assert.Equal(654, result1);
        Assert.Equal(7896314, result2);
        Assert.Equal(123456789, result3);
        Assert.Equal(9874564789631, result4);
    }

    [Fact]
    public void Base36ToBase10_ThrowsException_WhenInvalidBase36CharacterIsUsed()
    {
        var converter = new BaseConverter.PandaBaseConverter();
        Assert.Throws<ArgumentException>(() => converter.Base36ToBase10("21i3v#"));
    }
}