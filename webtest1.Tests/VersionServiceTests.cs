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
        public async Task VersionService_GetVersionCategory_Success()
        {
            const string testUri = "http://localhost/api/Version";
            const string resultString = "1234";   

            // Arrange                          
            SetupHttpClientFactory(System.Net.HttpStatusCode.OK, resultString);            
            var logger = new Mock<ILogger<VersionService>>();                   
            var options = new Mock<IOptions<DALOptions>>();                         
            options.Setup(x => x.Value.CategoryAbout).Returns(testUri).Verifiable();
            
            // Act
            var versionService = new VersionService(httpClientFactory.Object, logger.Object, options.Object);
            var version = await versionService.GetVersionCategory();
            
            // Evaluate
            version.Should().BeEquivalentTo(resultString); 
            httpMessageHandler.Verify();
            httpClientFactory.Verify();   
            options.Verify();            
        }

        [Fact]
        public async Task VersionService_GetVersionCategory_Failure()
        {
            const string testUri = "http://localhost/api/Version";
            const string resultString = "1234";   

            // Arrange                          
            SetupHttpClientFactory(System.Net.HttpStatusCode.NotFound, resultString);            
            var logger = new Mock<ILogger<VersionService>>();                   
            var options = new Mock<IOptions<DALOptions>>();                         
            options.Setup(x => x.Value.CategoryAbout).Returns(testUri).Verifiable();
            
            // Act
            var versionService = new VersionService(httpClientFactory.Object, logger.Object, options.Object);
            var version = await versionService.GetVersionCategory();
            
            // Evaluate
            version.Should().NotBeEquivalentTo(resultString); 
            version.Should().BeNull();
            httpMessageHandler.Verify();
            httpClientFactory.Verify();       
            options.Verify();        
        }          

        [Fact]
        public async Task VersionService_GetVersionCustomer_Success()
        {
            const string testUri = "http://localhost/api/Version";
            const string resultString = "1234";   

            // Arrange                          
            SetupHttpClientFactory(System.Net.HttpStatusCode.OK, resultString);            
            var logger = new Mock<ILogger<VersionService>>();                   
            var options = new Mock<IOptions<DALOptions>>();                         
            options.Setup(x => x.Value.CustomerAbout).Returns(testUri).Verifiable();
            
            // Act
            var versionService = new VersionService(httpClientFactory.Object, logger.Object, options.Object);
            var version = await versionService.GetVersionCustomer();
            
            // Evaluate
            version.Should().BeEquivalentTo(resultString); 
            httpMessageHandler.Verify();
            httpClientFactory.Verify();   
            options.Verify();            
        }

        [Fact]                
        public async Task VersionService_GetVersionCustomer_Failure()
        {
            const string testUri = "http://localhost/api/Version";
            const string resultString = "1234";   

            // Arrange                          
            SetupHttpClientFactory(System.Net.HttpStatusCode.NotFound, resultString);            
            var logger = new Mock<ILogger<VersionService>>();                   
            var options = new Mock<IOptions<DALOptions>>();                         
            options.Setup(x => x.Value.CustomerAbout).Returns(testUri).Verifiable();
            
            // Act
            var versionService = new VersionService(httpClientFactory.Object, logger.Object, options.Object);
            var version = await versionService.GetVersionCustomer();
            
            // Evaluate
            version.Should().NotBeEquivalentTo(resultString); 
            version.Should().BeNull();
            httpMessageHandler.Verify();
            httpClientFactory.Verify();       
            options.Verify();        
        }            

        [Fact]
        public async Task VersionService_GetVersionProduct_Success()
        {
            const string testUri = "http://localhost/api/Version";
            const string resultString = "1234";   

            // Arrange                          
            SetupHttpClientFactory(System.Net.HttpStatusCode.OK, resultString);            
            var logger = new Mock<ILogger<VersionService>>();                   
            var options = new Mock<IOptions<DALOptions>>();                         
            options.Setup(x => x.Value.ProductAbout).Returns(testUri).Verifiable();
            
            // Act
            var versionService = new VersionService(httpClientFactory.Object, logger.Object, options.Object);
            var version = await versionService.GetVersionProduct();
            
            // Evaluate
            version.Should().BeEquivalentTo(resultString); 
            httpMessageHandler.Verify();
            httpClientFactory.Verify();   
            options.Verify();            
        }

        [Fact]                
        public async Task VersionService_GetVersionProduct_Failure()
        {
            const string testUri = "http://localhost/api/Version";
            const string resultString = "1234";   

            // Arrange                          
            SetupHttpClientFactory(System.Net.HttpStatusCode.NotFound, resultString);            
            var logger = new Mock<ILogger<VersionService>>();                   
            var options = new Mock<IOptions<DALOptions>>();                         
            options.Setup(x => x.Value.ProductAbout).Returns(testUri).Verifiable();
            
            // Act
            var versionService = new VersionService(httpClientFactory.Object, logger.Object, options.Object);
            var version = await versionService.GetVersionProduct();
            
            // Evaluate
            version.Should().NotBeEquivalentTo(resultString); 
            version.Should().BeNull();
            httpMessageHandler.Verify();
            httpClientFactory.Verify();       
            options.Verify();        
        }        

        [Fact]
        public async Task VersionService_GetVersionEmployee_Success()
        {
            const string testUri = "http://localhost/api/Version";
            const string resultString = "1234";   

            // Arrange                          
            SetupHttpClientFactory(System.Net.HttpStatusCode.OK, resultString);            
            var logger = new Mock<ILogger<VersionService>>();                   
            var options = new Mock<IOptions<DALOptions>>();                         
            options.Setup(x => x.Value.EmployeeAbout).Returns(testUri).Verifiable();
            
            // Act
            var versionService = new VersionService(httpClientFactory.Object, logger.Object, options.Object);
            var version = await versionService.GetVersionEmployee();
            
            // Evaluate
            version.Should().BeEquivalentTo(resultString); 
            httpMessageHandler.Verify();
            httpClientFactory.Verify();   
            options.Verify();            
        }

        [Fact]                
        public async Task VersionService_GetVersionEmployee_Failure()
        {
            const string testUri = "http://localhost/api/Version";
            const string resultString = "1234";   

            // Arrange                          
            SetupHttpClientFactory(System.Net.HttpStatusCode.NotFound, resultString);            
            var logger = new Mock<ILogger<VersionService>>();                   
            var options = new Mock<IOptions<DALOptions>>();                         
            options.Setup(x => x.Value.EmployeeAbout).Returns(testUri).Verifiable();
            
            // Act
            var versionService = new VersionService(httpClientFactory.Object, logger.Object, options.Object);
            var version = await versionService.GetVersionEmployee();
            
            // Evaluate
            version.Should().NotBeEquivalentTo(resultString); 
            version.Should().BeNull();
            httpMessageHandler.Verify();
            httpClientFactory.Verify();       
            options.Verify();        
        }                
    }          
}
