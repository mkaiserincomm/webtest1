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

namespace webtest1.Tests
{
    public class VersionServiceTest
    {
        [Fact]
        public void Test1()
        {
            const string testUri = "http://localhost/api/Version";
            const string resultString = "1234";

            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(resultString);
            writer.Flush();
            stream.Position = 0;

            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StreamContent(stream)
            };

            var httpMessageHandler = new Mock<HttpMessageHandler>();
            httpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync", 
                    ItExpr.IsAny<HttpRequestMessage>(),                    
                    ItExpr.IsNull<CancellationToken>()                    
                )
                .ReturnsAsync(httpResponseMessage);
        
            var clientFactory = new Mock<IHttpClientFactory>();
            clientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(new HttpClient(httpMessageHandler.Object)).Verifiable("Client Not Created");

            var logger = new Mock<ILogger<VersionService>>();       
            
            var options = new Mock<IOptions<DALOptions>>();                         
            options.Setup(x => x.Value.About).Returns(testUri);
            
            var versionService = new VersionService(clientFactory.Object, logger.Object, options.Object);

            var version = versionService.GetVersion();
            Console.WriteLine(version.Result);
    
        }
    }
}
