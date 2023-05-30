# Base Converter

This project provides a class called **PandaBaseConverter** that can convert a number from base 10 to base 36 and vice versa. This project is available as a NuGet package as well.

## Usage

To use the **PandaBaseConverter** class, first create an instance of it:

```cs
var baseConverter = new PandaBaseConverter();
```

## Base 10 to Base 36

To convert a number from base 10 to base 36, call the **Base10ToBase36** method, passing in the number as a **long**

```cs
long number = 12345;
string base36Number = baseConverter.Base10ToBase36(number);
```

The resulting **base36Number** will be a string representation of the number in base 36.

## Base 36 to Base 10

To convert a number from base 36 to base 10, call the **Base36ToBase10** method, passing in the number as a **string**:

```cs
string base36Number = "2n9";
long number = baseConverter.Base36ToBase10(base36Number);
```

The resulting **number** will be the decimal representation of the number in base 36.

## Using in DTOs

```csharp
public class MyDto
{
    [JsonConverter(typeof(PandaJsonBaseConverter))]
    public long Id { get; set; }
}
```

In this case we will work in code with Id as long, but in json it will be as base 36 string.

## Configuration

By default, the **PandaBaseConverter** class uses the characters "0123456789abcdefghijklmnopqrstuvwxyz" for base 36 conversion. You can configure the character set used by setting the ***BASE36_CHARS** environment variable to a string containing the desired characters.

```cs
export BASE36_CHARS="A32145789U0BCDEFGHIJKLMNOPQRSTVWXYZ6"
```

The resulting **number** will be the decimal representation of the number in base 36.

## Error Handling

If the input to either conversion method is invalid, an **ArgumentException** will be thrown with an appropriate error message. The message will also be printed to the console.

