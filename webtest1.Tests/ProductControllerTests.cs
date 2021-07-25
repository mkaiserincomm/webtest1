using Xunit;
using Xunit.Abstractions;
using Moq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using FluentAssertions;
using webtest1.Controllers;
using webtest1.Data;
using webtest1.Tests.Mock;
using webtest1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using webtest1.Tests.TestData;

namespace webtest1.Tests
{
    public class ProductControllerTests
    {
        private readonly ITestOutputHelper _output;        
        private readonly List<Product> _dataServiceSampleData;

        public ProductControllerTests(ITestOutputHelper output)
        {
            _output = output;    
            _dataServiceSampleData = GetProductTestData.TestData;
        }

        [Fact]
        public async Task ProductController_Index_Success()
        {
            // Arrange
            var logger = new Mock<ILogger<ProductController>>();                   
            var dataService = DataServiceMockExtensions.GetDataServceMock<Product>().AddGet(_dataServiceSampleData);    
            var categoryDataService = DataServiceMockExtensions.GetDataServceMock<Category>().AddGet(GetCategoryTestData.TestData);              

            // Act 
            var controller = new ProductController(logger.Object, dataService.Object, categoryDataService.Object);
            var result = await controller.Index();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            dataService.Verify();

            var model = ((ViewResult)result).Model;
            model.Should().NotBeNull();
            model.Should().BeOfType<DataViewModel<Product>>();            

            var dataViewModel = ((DataViewModel<Product>)model);            
            dataViewModel.Data.Should().BeEquivalentTo(_dataServiceSampleData);            
        }       

        [Fact]
        public async Task ProductController_ProductList_Success()
        {
            // Arrange
            var logger = new Mock<ILogger<ProductController>>();                   
            var dataService = DataServiceMockExtensions.GetDataServceMock<Product>().AddGet(_dataServiceSampleData);  
            var categoryDataService = DataServiceMockExtensions.GetDataServceMock<Category>().AddGet(GetCategoryTestData.TestData);                  

            // Act 
            var controller = new ProductController(logger.Object, dataService.Object, categoryDataService.Object);
            var result = await controller.ProductList();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            dataService.Verify();

            var model = ((ViewResult)result).Model;
            model.Should().NotBeNull();
            model.Should().BeOfType<DataViewModel<Product>>();            

            var dataViewModel = ((DataViewModel<Product>)model);            
            dataViewModel.Data.Should().BeEquivalentTo(_dataServiceSampleData);            
        }        

        [Theory]        
        [ClassData(typeof(GetProductTestCases))]
        public async Task ProductController_GetProduct_Success(DataViewModel<Product> testModel, string viewName, bool stateIsValid, bool success, string description)
        {
            _output.WriteLine("Description: {0}", description);

            // Arrange
            var logger = new Mock<ILogger<ProductController>>();                               
            var dataService = DataServiceMockExtensions.GetDataServceMock<Product>();
            switch (testModel.Action)
            {
                case "updatedata":
                    dataService.AddPut(_dataServiceSampleData, success);  
                    break;
                case "insertdata":
                    dataService.AddPost(_dataServiceSampleData, success);  
                    break;
                case "edit":
                    dataService.AddGetById(_dataServiceSampleData);                    
                    break;
                case "insert":
                    break;
                case "delete":
                    dataService.AddDelete();
                    break;
                default:
                    dataService.AddGet(_dataServiceSampleData);
                    break;
            }
            var categoryDataService = DataServiceMockExtensions.GetDataServceMock<Category>().AddGet(GetCategoryTestData.TestData);  
                            

            // Act             
            var controller = new ProductController(logger.Object, dataService.Object, categoryDataService.Object);
            if (!stateIsValid) controller.ModelState.AddModelError("ProductName", "Product Name is Required");      
            var result = await controller.GetProduct(testModel);            

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();            

            var viewResult = ((ViewResult)result);
            viewResult.ViewName.Should().BeEquivalentTo(viewName);

            var model = viewResult.Model;
            model.Should().NotBeNull();                        
            model.Should().BeOfType<DataViewModel<Product>>();                        
        }                        
    }    

    
}