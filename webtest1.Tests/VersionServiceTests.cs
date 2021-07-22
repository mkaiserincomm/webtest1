using System;
using Xunit;
using Moq;
using Moq.Protected;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using webtest1.Options;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using FluentAssertions;
using webtest1.Services;

namespace webtest1.Tests
{
    public class VersionServiceTest
    {
        private Mock<HttpMessageHandler> httpMessageHandler;
        private Mock<IHttpClientFactory> httpClientFactory;
        
        [Fact]
        public async Task VersionService_GetVersion_Success()
        {
            const string testUri = "http://localhost/api/Version";
            const string resultString = "1234";   

            // Setup                            
            SetupHttpClientFactory(System.Net.HttpStatusCode.OK, resultString);            
            var logger = new Mock<ILogger<VersionService>>();                   
            var options = new Mock<IOptions<DALOptions>>();                         
            options.Setup(x => x.Value.About).Returns(testUri);
            
            // Act
            var versionService = new VersionService(httpClientFactory.Object, logger.Object, options.Object);
            var version = await versionService.GetVersion();
            
            // Evaluate
            version.Should().BeEquivalentTo(resultString); 
            httpMessageHandler.Verify();
            httpClientFactory.Verify();               
        }

        [Fact]
        public async Task VersionService_GetVersion_Failure()
        {
            const string testUri = "http://localhost/api/Version";
            const string resultString = "1234";   

            // Setup                            
            SetupHttpClientFactory(System.Net.HttpStatusCode.NotFound, resultString);            
            var logger = new Mock<ILogger<VersionService>>();                   
            var options = new Mock<IOptions<DALOptions>>();                         
            options.Setup(x => x.Value.About).Returns(testUri);
            
            // Act
            var versionService = new VersionService(httpClientFactory.Object, logger.Object, options.Object);
            var version = await versionService.GetVersion();
            
            // Evaluate
            version.Should().NotBeEquivalentTo(resultString); 
            version.Should().BeNull();
            httpMessageHandler.Verify();
            httpClientFactory.Verify();               
        }        

        private void SetupHttpClientFactory(System.Net.HttpStatusCode statusCode, string resultString)
        {
            httpMessageHandler = new Mock<HttpMessageHandler>();
            httpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync", 
                    ItExpr.IsAny<HttpRequestMessage>(),                    
                    ItExpr.IsAny<CancellationToken>()                    
                )
                .ReturnsAsync(new HttpResponseMessage() {
                    StatusCode = statusCode,
                    Content = new StringContent(resultString),
                })
                .Verifiable("Message Handler was not Called"); 
            
            httpClientFactory = new Mock<IHttpClientFactory>();
            httpClientFactory
                .Setup(x => x.CreateClient(It.IsAny<string>()))
                .Returns(new HttpClient(httpMessageHandler.Object))                
                .Verifiable("Client Not Created");            
        }       
    }        
}
