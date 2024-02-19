using Microsoft.Extensions.Logging;
using NSubstitute;
using PaymentGateway.Api.Controllers;

namespace PaymentGateway.Test
{
    public class ApiControllerTest
    {
        private readonly ILogger<ApiController> _logger;

        public ApiControllerTest()
        {
            _logger = Substitute.For<ILogger<ApiController>>();
        }
    }
}