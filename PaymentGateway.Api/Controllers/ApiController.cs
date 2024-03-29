using Microsoft.AspNetCore.Mvc;

namespace PaymentGateway.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class ApiController : ControllerBase
    {
        private readonly ILogger<ApiController> _logger;

        public ApiController(ILogger<ApiController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "test")]
        public Task<string> TestAsync()
        {
            return Task.FromResult("Hello World");
        }
    }
}