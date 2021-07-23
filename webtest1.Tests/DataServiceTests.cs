using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using webtest1.Data;
using webtest1.Options;
using webtest1.Services;
using Xunit;

namespace webtest1.Tests
{
    public class DataServiceTests : BaseServiceTest
    {
        [Fact]
        public async Task DataService_Get_Success()
        {            
            const string testUri = "http://localhost/api/Category";
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
            var logger = new Mock<ILogger<DataService<Category>>>();                   
            var options = new Mock<IOptions<DALOptions>>();                                     
            options.Setup(x => x.Value.GetValue(It.Is<string>(p => p == "Category"))).Returns(testUri).Verifiable();            
            
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
        public async Task DataService_GetById_Success()
        {
            const string testUri = "http://localhost/api/Category";
            const int id = 1;
            var sampleData = new Category{
                categoryId = 1,
                categoryName = "First",
                description = "description of first",
                picture = new byte[] { 1, 2, 3, 4 }
            };
              
            // Setup                            
            SetupHttpClientFactory(System.Net.HttpStatusCode.OK, JsonSerializer.Serialize(sampleData));            
            var logger = new Mock<ILogger<DataService<Category>>>();                   
            var options = new Mock<IOptions<DALOptions>>();                                     
            options.Setup(x => x.Value.GetValue(It.Is<string>(p => p == "Category"))).Returns(testUri).Verifiable();            
            
            // Act
            var versionService = new DataService<Category>(httpClientFactory.Object, logger.Object, options.Object);
            var data = await versionService.GetById(id.ToString());
            
            // Evaluate
            data.Should().BeEquivalentTo(sampleData); 
            sampleData.categoryId.Should().Be(id);
            httpMessageHandler.Verify();
            httpClientFactory.Verify();
            options.Verify();
        }   

        [Fact]
        public async Task DataService_GetById_Failure()
        {
            const string testUri = "http://localhost/api/Category";
            const int id = 999;            
              
            // Setup                            
            SetupHttpClientFactory(System.Net.HttpStatusCode.NotFound);            
            var logger = new Mock<ILogger<DataService<Category>>>();                   
            var options = new Mock<IOptions<DALOptions>>();                                     
            options.Setup(x => x.Value.GetValue(It.Is<string>(p => p == "Category"))).Returns(testUri).Verifiable();            
            
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
            await Task.CompletedTask;
        }   

        [Fact]
        public async Task DataService_Post_Success()
        {
            await Task.CompletedTask;
        }   

        [Fact]
        public async Task DataService_Delete_Success()
        {
            await Task.CompletedTask;
        }   
    }
}