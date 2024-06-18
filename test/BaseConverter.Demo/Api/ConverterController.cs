using BaseConverter.Attributes;
using BaseConverter.Demo.Models;
using Microsoft.AspNetCore.Mvc;

namespace BaseConverter.Demo.Api;

[ApiController]
[Route("controller")]
public class ConverterController : Controller
{
    [HttpGet("query")]
    public IActionResult GetFromQuery([ParameterBaseConverter] [FromQuery] long id)
    {
        return Ok(id);
    }

    [HttpGet("query-nullable")]
    public IActionResult GetFromQuery([ParameterBaseConverter] [FromQuery] long? id)
    {
        return Ok(id);
    }

    [HttpGet("path/{id}")]
    public IActionResult GetFromPath([ParameterBaseConverter] long id)
    {
        return Ok(id);
    }

    [HttpGet("path-nullable/{id}")]
    public IActionResult GetFromPath([ParameterBaseConverter] long? id)
    {
        return Ok(id);
    }

    [HttpPost("body")]
    public IActionResult GetFromBody([FromBody] Body request)
    {
        return Ok(BodyResponse.FromTestConverterModel(request));
    }
}