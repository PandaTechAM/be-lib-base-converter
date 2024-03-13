- [1. Pandatech.BaseConverter](#1-pandatechbaseconverter)
  - [1.1. Features](#11-features)
  - [1.2. Installation](#12-installation)
  - [1.3. Basic Usage](#13-basic-usage)
  - [1.4. Advanced Usage](#14-advanced-usage)
    - [1.4.1. Customizing Base 36 Character Set](#141-customizing-base-36-character-set)
    - [1.4.2. Integration with DTOs](#142-integration-with-dtos)
    - [1.4.3. Controller Parameter Binding](#143-controller-parameter-binding)
    - [1.4.4. Validation](#144-validation)
    - [1.4.5. Swagger Integration](#145-swagger-integration)
    - [1.4.6. Error Handling](#146-error-handling)
  - [1.5. Contributing](#15-contributing)
  - [1.6. License](#16-license)

# 1. Pandatech.BaseConverter

The `Pandatech.BaseConverter` library offers robust and flexible base conversion functionalities, enabling seamless
transformations between base 10 and base 36 numeral systems. Designed for simplicity and efficiency, it's an essential
tool for applications requiring numeral system conversions, particularly useful in scenarios like generating concise,
URL-friendly unique identifiers from numeric values. Available as a convenient NuGet package, it integrates effortlessly
into your .NET projects.

## 1.1. Features

- **Bi-directional Conversion**: Supports converting numbers from base 10 to base 36 and vice versa.
- **Custom Character Set Configuration**: Allows customization of the character set used for base 36 encoding, enabling
  unique identifier generation tailored to specific requirements.
- **Integration with Data Transfer Objects (DTOs)**: Facilitates the use of base 36 encoded strings in DTOs, enhancing
  API usability and readability.
- **Swagger Integration**: Ensures seamless integration with Swagger for API documentation and testing, with support for
  custom parameter converters.
- **Robust Validation**: Offers built-in validation for base 36 inputs, ensuring data integrity and error resilience.
- **Exception Handling**: Provides clear and informative error messages for invalid inputs, aiding in troubleshooting
  and debugging.

## 1.2. Installation

To include `Pandatech.BaseConverter` in your project, install it as a NuGet package:

```shell
Install-Package Pandatech.BaseConverter
```

## 1.3. Basic Usage

Converting from Base 10 to Base 36

```csharp
long number = 12345;
string base36Number = BaseConverter.PandaBaseConverter.Base10ToBase36(number);
// Output: base36Number = "9ix"
```

Converting from Base 36 to Base 10

```csharp
string base36Number = "9ix";
long number = BaseConverter.PandaBaseConverter.Base36ToBase10(base36Number);
// Output: number = 12345
```

## 1.4. Advanced Usage

### 1.4.1. Customizing Base 36 Character Set

You can customize the character set used for base 36 encoding to suit your application's needs, for not exposing actual
order:

```csharp
var builder = WebApplication.CreateBuilder(args);
var customCharset = "0123456789abcdefghijklmnopqrstuvwxyz";
builder.Services.ConfigureBaseConverter(customCharset);
```

### 1.4.2. Integration with DTOs

Decorate DTO properties with `[PandaPropertyBaseConverter]` to automatically handle base 36 encoding/decoding:

```csharp
public class MyDto
{
    [PandaPropertyBaseConverter]
    public long Id { get; set; }
}
```

### 1.4.3. Controller Parameter Binding

Use `[PandaParameterBaseConverter]` to automatically resolve base 36 encoded parameters in controller actions:

```csharp
[HttpGet("{id}")]
public async Task<ActionResult<MyDto>> Get([PandaParameterBaseConverter] long id)
{
    // Your logic here
}
```

### 1.4.4. Validation

Validate base 36 inputs using the `ValidateBase36Chars` extension method:

```csharp
string base36Number = "9ix";
bool isValid = base36Number.ValidateBase36Chars();
// Output: isValid = true
```

### 1.4.5. Swagger Integration

```csharp
builder.Services.AddSwaggerGen(
    options =>
    {
        options.ParameterFilter<PandaParameterBaseConverterAttribute>();
        options.SchemaFilter<PandaPropertyBaseConverterSwaggerFilter>();
    }
);
```

### 1.4.6. Error Handling

The library employs `ArgumentException` to signal invalid inputs, equipped with descriptive messages to facilitate
debugging:

```csharp
try
{
    var result = BaseConverter.PandaBaseConverter.Base36ToBase10("invalid-input");
}
catch (ArgumentException ex)
{
    Console.WriteLine(ex.Message);
    // Handle the exception as needed
}
```

## 1.5. Contributing

Contributions are welcome! If you have suggestions for improvements or encounter any issues, please feel free to open an
issue or submit a pull request.

## 1.6. License

This project is licensed under the MIT License. Feel free to use, modify, and distribute it as per the license terms.

Thank you for choosing `Pandatech.BaseConverter` for your base conversion needs!

