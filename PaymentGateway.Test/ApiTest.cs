using FluentAssertions;
using PaymentGateway.Api.Controllers;

namespace PaymentGateway.Test
{
    public class ApiTest
    {
        [Fact]
        public void Test1()
        {
            new ApiController(null).TestAsync().Result.Should().Be("Hello World");
        }
    }
}