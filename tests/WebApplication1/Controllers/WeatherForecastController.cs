using BaseConverter;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

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

    [HttpGet("[action]")]
    public IActionResult GetSome2(long i)
    {
        return Ok(i);
    }
}