using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using webtest1.Data;
using webtest1.Options;
using webtest1.Services;
using Xunit;

namespace webtest1.Tests
{
    public class DataServiceTests : BaseServiceTest
    {

        private const string testUri = "http://localhost/api/Category";

        protected Mock<ILogger<DataService<Category>>> logger = new Mock<ILogger<DataService<Category>>>();                   
        protected Mock<IOptions<DALOptions>> options = new Mock<IOptions<DALOptions>>();     

        public DataServiceTests()
        {
            options.Setup(x => x.Value.GetValue(It.Is<string>(p => p == "Category"))).Returns(testUri).Verifiable();            
        }

        [Fact]
        public async Task DataService_Get_Success()
        {            
            
            var sampleData = new List<Category>
            {
                new Category{
                    categoryId = 1,
                    categoryName = "First",
                    description = "description of first",
                    picture = new byte[] { 1, 2, 3, 4 }
                },
                new Category{
                    categoryId = 2,
                    categoryName = "Second",
                    description = "description of second",
                    picture = new byte[] { 5, 6, 7, 8 }
                }
            };

            // Setup                            
            SetupHttpClientFactory(System.Net.HttpStatusCode.OK, JsonSerializer.Serialize(sampleData));                                                                       
            
            // Act
            var versionService = new DataService<Category>(httpClientFactory.Object, logger.Object, options.Object);
            var data = await versionService.Get();
            
            // Evaluate
            data.Should().BeEquivalentTo(sampleData); 
            httpMessageHandler.Verify();
            httpClientFactory.Verify();
            options.Verify();
        }   

        [Fact]
        public async Task DataService_Get_Failure()
        {                                    
            // Setup                            
            SetupHttpClientFactory(System.Net.HttpStatusCode.InternalServerError);                                                                       
            
            // Act
            var versionService = new DataService<Category>(httpClientFactory.Object, logger.Object, options.Object);
            var data = await versionService.Get();
            
            // Evaluate
            data.Should().BeNull();
            httpMessageHandler.Verify();
            httpClientFactory.Verify();
            options.Verify();
        }   

        [Fact]
        public async Task DataService_GetById_Success()
        {            
            const int id = 1;
            var sampleData = new Category{
                categoryId = 1,
                categoryName = "First",
                description = "description of first",
                picture = new byte[] { 1, 2, 3, 4 }
            };
              
            // Setup                            
            SetupHttpClientFactory(System.Net.HttpStatusCode.OK, JsonSerializer.Serialize(sampleData));                                    
            
            // Act
            var versionService = new DataService<Category>(httpClientFactory.Object, logger.Object, options.Object);
            var data = await versionService.GetById(id.ToString());
            
            // Evaluate
            data.Should().BeEquivalentTo(sampleData); 
            data.categoryId.Should().Be(id);
            httpMessageHandler.Verify();
            httpClientFactory.Verify();
            options.Verify();
        }   

        [Fact]
        public async Task DataService_GetById_Failure()
        {         
            const int id = 999;            
              
            // Setup                            
            SetupHttpClientFactory(System.Net.HttpStatusCode.NotFound);                                                                    
            
            // Act
            var versionService = new DataService<Category>(httpClientFactory.Object, logger.Object, options.Object);
            var data = await versionService.GetById(id.ToString());
            
            // Evaluate
            data.Should().BeNull();            
            httpMessageHandler.Verify();
            httpClientFactory.Verify();
            options.Verify();
        }   

        [Fact]
        public async Task DataService_Put_Success()
        {
            var sampleData = new Category{
                categoryId = 1,
                categoryName = "First",
                description = "description of first",
                picture = new byte[] { 1, 2, 3, 4 }
            };
              
            // Setup                            
            SetupHttpClientFactory(System.Net.HttpStatusCode.OK);                                    
            
            // Act
            var versionService = new DataService<Category>(httpClientFactory.Object, logger.Object, options.Object);
            var data = await versionService.Put(sampleData.categoryId.ToString(), sampleData);
            
            // Evaluate
            data.Should().BeTrue();            
            httpMessageHandler.Verify();
            httpClientFactory.Verify();
            options.Verify();
        }   

        [Fact]
        public async Task DataService_Put_Failure()
        {
            var sampleData = new Category{
                categoryId = 1,
                categoryName = "First",
                description = "description of first",
                picture = new byte[] { 1, 2, 3, 4 }
            };
              
            // Setup                            
            SetupHttpClientFactory(System.Net.HttpStatusCode.InternalServerError);                                    
            
            // Act
            var versionService = new DataService<Category>(httpClientFactory.Object, logger.Object, options.Object);
            var data = await versionService.Put(sampleData.categoryId.ToString(), sampleData);
            
            // Evaluate
            data.Should().BeFalse();            
            httpMessageHandler.Verify();
            httpClientFactory.Verify();
            options.Verify();
        }   

        [Fact]
        public async Task DataService_Post_Success()
        {
            var sampleData = new Category{
                categoryId = 1,
                categoryName = "First",
                description = "description of first",
                picture = new byte[] { 1, 2, 3, 4 }
            };
              
            // Setup                            
            SetupHttpClientFactory(System.Net.HttpStatusCode.Created);                                    
            
            // Act
            var versionService = new DataService<Category>(httpClientFactory.Object, logger.Object, options.Object);
            var data = await versionService.Post(sampleData);
            
            // Evaluate
            data.Should().BeTrue();            
            httpMessageHandler.Verify();
            httpClientFactory.Verify();
            options.Verify();
        }   

        [Fact]
        public async Task DataService_Post_Failure()
        {
            var sampleData = new Category{
                categoryId = 1,
                categoryName = "First",
                description = "description of first",
                picture = new byte[] { 1, 2, 3, 4 }
            };
              
            // Setup                            
            SetupHttpClientFactory(System.Net.HttpStatusCode.InternalServerError);                                    
            
            // Act
            var versionService = new DataService<Category>(httpClientFactory.Object, logger.Object, options.Object);
            var data = await versionService.Post(sampleData);
            
            // Evaluate
            data.Should().BeFalse();            
            httpMessageHandler.Verify();
            httpClientFactory.Verify();
            options.Verify();
        }   

        [Fact]               
        public async Task DataService_Delete_Success()
        {
            const int id = 1;                        

            // Setup                            
            SetupHttpClientFactory(System.Net.HttpStatusCode.OK);                                    
            
            // Act
            var versionService = new DataService<Category>(httpClientFactory.Object, logger.Object, options.Object);
            var result = await versionService.Delete(id.ToString());
            
            // Evaluate
            result.Should().BeTrue();
            httpMessageHandler.Verify();
            httpClientFactory.Verify();
            options.Verify();
        }   

        [Fact]               
        public async Task DataService_Delete_Failure()
        {
            const int id = 1;                        

            // Setup                            
            SetupHttpClientFactory(System.Net.HttpStatusCode.NotFound);                                    
            
            // Act
            var versionService = new DataService<Category>(httpClientFactory.Object, logger.Object, options.Object);
            var result = await versionService.Delete(id.ToString());
            
            // Evaluate
            result.Should().BeFalse();
            httpMessageHandler.Verify();
            httpClientFactory.Verify();
            options.Verify();
        }   
    }
}