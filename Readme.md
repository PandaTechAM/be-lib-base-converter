# Pandatech.BaseConverter

`Pandatech.BaseConverter` is a powerful library designed for seamless base conversion between base 10 and base 36
numeral systems. It addresses the common need in software development to obfuscate database primary keys stored as long
integers by converting them into base 36 encoded strings, enhancing data confidentiality and avoiding direct exposure of
entity IDs.

## Features

- **Bidirectional Conversion**: Convert numbers between base 10 and base 36.
- **Custom Character Set**: Configure the base 36 character set for tailored encoding.
- **DTO Integration**: Simplify base 36 string usage in Data Transfer Objects (DTOs).
- **Batch Conversion**: Convert lists of numbers between base 10 and base 36 efficiently.
- **Swagger Support**: Integrate with Swagger for API documentation and testing.
- **Validation**: Ensure data integrity with robust validation for base 36 inputs.
- **Error Handling**: Clear and informative exceptions for invalid inputs.

## Installation

Add `Pandatech.BaseConverter` to your project via NuGet:

```shell
Install-Package Pandatech.BaseConverter

```

## Basic Usage

### Converting between Base 10 and Base 36

```csharp
long number = 12345;
string base36Number = PandaBaseConverter.Base10ToBase36(number);
// Output: "9ix"

string base36Number = "9ix";
long? number = PandaBaseConverter.Base36ToBase10(base36Number);
// Output: 12345

long number = PandaBaseConverter.Base36ToBase10NotNull(base36Number);
// Output: 12345

var numbers = new List<long> { 12345, 67890 };
var base36Numbers = PandaBaseConverter.Base10ListToBase36List(numbers);
// Output: ["9ix", "1bqj"]

var base36Numbers = new List<string> { "9ix", "1bqj" };
var numbers = PandaBaseConverter.Base36ListToBase10List(base36Numbers);
// Output: [12345, 67890]
```

## Advanced Usage

### Customizing Base 36 Character Set

Customize the base 36 character set for your needs:

```csharp
var builder = WebApplication.CreateBuilder(args);
var customCharset = "0123456789abcdefghijklmnopqrstuvwxyz";
builder.Services.ConfigureBaseConverter(customCharset);
```

### DTO Integration

Decorate DTO properties to handle automatic base 36 encoding/decoding:

```csharp
public class MyDto
{
    [PropertyBaseConverter]
    public long Id { get; set; }
}
```

### Minimal API Parameter Binding

Use `QueryBaseConverter` and `PathBaseConverter` to automatically convert base 36 encoded parameters in minimal APIs:

```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var groupApp = app.MapGroup("minimal");
groupApp.MapGet("query", ([FromQuery] long id) => id)
        .QueryBaseConverter("id");

groupApp.MapGet("path/{id}", (long id) => id)
        .PathBaseConverter("id");

app.Run();
```

### Controller Parameter Binding

Resolve base 36 encoded parameters in controller actions using `[ParameterBaseConverter]`:

```csharp
[ApiController]
[Route("api")]
public class ConverterController : ControllerBase
{
    [HttpGet("query")]
    public IActionResult GetFromQuery([ParameterBaseConverter, FromQuery] long id)
    {
        return Ok(id);
    }

    [HttpGet("path/{id}")]
    public IActionResult GetFromPath([ParameterBaseConverter] long id)
    {
        return Ok(id);
    }
}
```

### Swagger Integration

Integrate with Swagger for enhanced API documentation:

```csharp
builder.Services.AddSwaggerGen(
    options =>
    {
        options.ParameterFilter<ParameterBaseConverter>();
        options.SchemaFilter<PropertyBaseConverterFilter>();
    }
);
```

## Error Handling

`Pandatech.BaseConverter` provides informative exceptions for handling various errors. Here's how you can manage them:

### Supported Exceptions

- **`UnsupportedCharacterException`**: Thrown when the input contains characters not supported by the current base 36
  character set.

```csharp
    try
    {
        var result = PandaBaseConverter.Base36ToBase10("invalid-input");
    }
    catch (UnsupportedCharacterException ex)
    {
        Console.WriteLine(ex.FullMessage);
        // Output: "Message: Input contains unsupported characters. with Value: invalid-input"
    }
   ```

- **`InputValidationException`**: Thrown when input validation fails, such as a negative number for base 10 to base 36
  conversion.

```csharp
    try
    {
        var base36Number = PandaBaseConverter.Base10ToBase36(-12345);
    }
    catch (InputValidationException ex)
    {
        Console.WriteLine(ex.FullMessage);
        // Output: "Message: Number must be non-negative. with Value: -12345"
    }
```

- **`BaseConverterException`**: The base exception class for all custom exceptions in this library, providing
  a `FullMessage` property that includes both the message and the associated value.

```csharp
    try
    {
        // Some operation that might fail
    }
    catch (BaseConverterException ex)
    {
        Console.WriteLine(ex.FullMessage);
    }
 ```

These exceptions ensure that your applications can gracefully handle and debug errors related to base conversions.

## Contributing

Contributions are welcome! If you have suggestions for improvements or encounter any issues, please feel free to open an
issue or submit a pull request.

## License

This project is licensed under the MIT License. Feel free to use, modify, and distribute it as per the license terms.

Thank you for choosing `Pandatech.BaseConverter` for your base conversion needs!

