using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using webtest1.Options;
using webtest1.Services;
using Xunit;

namespace webtest1.Tests
{
    public class VersionServiceTest : BaseServiceTest
    {            
        [Fact]
        public async Task VersionService_GetVersion_Success()
        {
            const string testUri = "http://localhost/api/Version";
            const string resultString = "1234";   

            // Arrange                          
            SetupHttpClientFactory(System.Net.HttpStatusCode.OK, resultString);            
            var logger = new Mock<ILogger<VersionService>>();                   
            var options = new Mock<IOptions<DALOptions>>();                         
            options.Setup(x => x.Value.About).Returns(testUri).Verifiable();
            
            // Act
            var versionService = new VersionService(httpClientFactory.Object, logger.Object, options.Object);
            var version = await versionService.GetVersion();
            
            // Evaluate
            version.Should().BeEquivalentTo(resultString); 
            httpMessageHandler.Verify();
            httpClientFactory.Verify();   
            options.Verify();            
        }

        [Fact]
        public async Task VersionService_GetVersion_Failure()
        {
            const string testUri = "http://localhost/api/Version";
            const string resultString = "1234";   

            // Arrange                          
            SetupHttpClientFactory(System.Net.HttpStatusCode.NotFound, resultString);            
            var logger = new Mock<ILogger<VersionService>>();                   
            var options = new Mock<IOptions<DALOptions>>();                         
            options.Setup(x => x.Value.About).Returns(testUri).Verifiable();
            
            // Act
            var versionService = new VersionService(httpClientFactory.Object, logger.Object, options.Object);
            var version = await versionService.GetVersion();
            
            // Evaluate
            version.Should().NotBeEquivalentTo(resultString); 
            version.Should().BeNull();
            httpMessageHandler.Verify();
            httpClientFactory.Verify();       
            options.Verify();        
        }          
    }      
}
