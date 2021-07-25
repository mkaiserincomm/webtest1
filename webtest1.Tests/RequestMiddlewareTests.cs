using Xunit;
using Xunit.Abstractions;
using FluentAssertions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;
using Microsoft.Extensions.Logging;

namespace webtest1.Tests
{
    public class RequestMiddlewareTests
    {

        private readonly ITestOutputHelper _output;                

        public RequestMiddlewareTests(ITestOutputHelper output)
        {
            _output = output;                
        }

        [Fact]
        public async Task RequestMiddleware_Invoke_Success()
        {
            // Arrange
            var ctx = new DefaultHttpContext();
            RequestDelegate next = (HttpContext hc) => Task.CompletedTask;
            var loggerFactory = new Mock<ILoggerFactory>();         
            loggerFactory.Setup(x => x.CreateLogger(It.IsAny<string>())).Verifiable();   

            // Act
            var requestMiddleware = new RequestMiddleware(next, loggerFactory.Object);
            await requestMiddleware.Invoke(ctx);
            // Assert
            loggerFactory.Verify();                        
            var metricsText = await requestMiddleware.GetMetrics();
            metricsText.Should().ContainAll(
                new string[] {
                    "http_request_elapsedms{path=\"\",method=\"\"} 0",
                    "http_request_count{path=\"\",method=\"\",status=\"200\"} 1"
                }
            );
                        
        }

        [Fact]        
        public async Task RequestMiddleware_Invoke_Failure()
        {
            // Arrange
            var ctx = new DefaultHttpContext();
            RequestDelegate next = (HttpContext hc) => Task.Run(() => throw new RequestMiddlewareTestException());
            var loggerFactory = new Mock<ILoggerFactory>();         
            loggerFactory.Setup(x => x.CreateLogger(It.IsAny<string>())).Verifiable();   

            // Act
            var requestMiddleware = new RequestMiddleware(next, loggerFactory.Object);
            var RequestMiddlewareTestException = await Assert.ThrowsAsync<RequestMiddlewareTestException>(async () => await requestMiddleware.Invoke(ctx));
            
            // Assert                        
            loggerFactory.Verify();                        
            var metricsText = await requestMiddleware.GetMetrics();
            metricsText.Should().ContainAll(
                new string[] {
                    "http_request_elapsedms{path=\"\",method=\"\"}",
                    "http_request_count{path=\"\",method=\"\",status=\"500\"}"
                }
            );
                        
        }
        
        public class RequestMiddlewareTestException : System.Exception
        {
            public RequestMiddlewareTestException() { }
            public RequestMiddlewareTestException(string message) : base(message) { }
            public RequestMiddlewareTestException(string message, System.Exception inner) : base(message, inner) { }
            protected RequestMiddlewareTestException(
                System.Runtime.Serialization.SerializationInfo info,
                System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        }
    }
}