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
    public class CategoryControllerTests
    {
        private readonly ITestOutputHelper _output;        
        private readonly List<Category> _dataServiceSampleData;

        public CategoryControllerTests(ITestOutputHelper output)
        {
            _output = output;    
            _dataServiceSampleData = GetCategoryTestData.TestData;
        }

        [Fact]
        public async Task CategoryController_Index_Success()
        {
            // Setup
            var logger = new Mock<ILogger<CategoryController>>();                   
            var dataService = DataServiceMockExtensions.GetDataServceMock<Category>().AddGet(_dataServiceSampleData);                  

            // Act 
            var controller = new CategoryController(logger.Object, dataService.Object);
            var result = await controller.Index();

            // Validate
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            dataService.Verify();

            var model = ((ViewResult)result).Model;
            model.Should().NotBeNull();
            model.Should().BeOfType<DataViewModel<Category>>();            

            var dataViewModel = ((DataViewModel<Category>)model);            
            dataViewModel.Data.Should().BeEquivalentTo(_dataServiceSampleData);            
        }       

        [Fact]
        public async Task CategoryController_CategoryList_Success()
        {
            // Setup
            var logger = new Mock<ILogger<CategoryController>>();                   
            var dataService = DataServiceMockExtensions.GetDataServceMock<Category>().AddGet(_dataServiceSampleData);                  

            // Act 
            var controller = new CategoryController(logger.Object, dataService.Object);
            var result = await controller.CategoryList();

            // Validate
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            dataService.Verify();

            var model = ((ViewResult)result).Model;
            model.Should().NotBeNull();
            model.Should().BeOfType<DataViewModel<Category>>();            

            var dataViewModel = ((DataViewModel<Category>)model);            
            dataViewModel.Data.Should().BeEquivalentTo(_dataServiceSampleData);            
        }        

        [Theory]        
        [ClassData(typeof(GetCategoryTestCases))]
        public async Task CategoryController_GetCategory_Success(DataViewModel<Category> testModel, string viewName, bool stateIsValid, bool success, string description)
        {
            _output.WriteLine("Description: {0}", description);

            // Setup
            var logger = new Mock<ILogger<CategoryController>>();                               
            var dataService = DataServiceMockExtensions.GetDataServceMock<Category>();
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
                            

            // Act             
            var controller = new CategoryController(logger.Object, dataService.Object);
            if (!stateIsValid) controller.ModelState.AddModelError("categoryName", "Category Name is Required");      
            var result = await controller.GetCategory(testModel);            

            // Validate
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();            

            var viewResult = ((ViewResult)result);
            viewResult.ViewName.Should().BeEquivalentTo(viewName);

            var model = viewResult.Model;
            model.Should().NotBeNull();                        
            model.Should().BeOfType<DataViewModel<Category>>();                        
        }                        
    }    

    
}