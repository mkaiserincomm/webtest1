using System;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using webtest1.Options;

namespace webtest1.Tests
{
    public class VersionServiceTest
    {
        [Fact]
        public void Test1()
        {
            var clientFactory = new Mock<IHttpClientFactory>();
            clientFactory.Setup(x => x.)

            var logger = new Mock<ILogger<VersionService>>();       
            
            var options = new Mock<IOptions<DALOptions>>();                         
            options.Setup(x => x.Value.About).Returns("http://localhost/api/Version");
            
            var versionService = new VersionService(clientFactory.Object, logger.Object, options.Object);

            var version = versionService.GetVersion();
            Console.WriteLine(version);
    
        }
    }
}
