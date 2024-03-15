using BaseConverter.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace BaseConverter.Demo.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet("[action]")]
    public IActionResult GetSome([PandaParameterBaseConverter] long i)
    {
        return Ok(i);
    }

    [HttpPost("[action]")]
    public IActionResult PostSome([FromBody] MyDataModel someClass)
    {
        var response = new Dictionary<string, long>()
        {
            { "MyLongValue", someClass.MyLongValue },
            { "MyNullableLongValue", (long)someClass.MyNullableLongValue! }
        };
        return Ok(response);
    }
}

public class MyDataModel
{
    [PandaPropertyBaseConverter] public long MyLongValue { get; set; }

    [PandaPropertyBaseConverter] public long? MyNullableLongValue { get; set; }
}