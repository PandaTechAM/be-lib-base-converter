using BaseConverter;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/")]
    public class SomeController : Controller
    {
        private ILogger<SomeController> _logger;

        public SomeController(ILogger<SomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("get-some")]
        public IActionResult GetSome([PandaParameterBaseConverter] long id)
        {
            var model = new SomeModel
            {
                NotNullProperty = id,
                NullableProperty = null,
            };

            _logger.LogInformation($"Not Nullable Property {model.NotNullProperty} and Nullable Property {model.NullableProperty}");

            return Ok($"{model.NotNullProperty} and {model.NullableProperty}");
        }

        [HttpPost("post-some")]
        public IActionResult PostSome(SomeModel model)
        {
            _logger.LogInformation($"Not Nullable Property {model.NotNullProperty} and Nullable Property {model.NullableProperty}");

            return Ok($"{model.NotNullProperty} and {model.NullableProperty}");
        }
    }
}
