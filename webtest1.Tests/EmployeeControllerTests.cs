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
    public class EmployeeControllerTests
    {
        private readonly ITestOutputHelper _output;        
        private readonly List<Employee> _dataServiceSampleData;

        public EmployeeControllerTests(ITestOutputHelper output)
        {
            _output = output;    
            _dataServiceSampleData = GetEmployeeTestData.TestData;
        }

        [Fact]
        public async Task EmployeeController_Index_Success()
        {
            // Arrange
            var logger = new Mock<ILogger<EmployeeController>>();                   
            var dataService = DataServiceMockExtensions.GetDataServceMock<Employee>().AddGet(_dataServiceSampleData);                  

            // Act 
            var controller = new EmployeeController(logger.Object, dataService.Object);
            var result = await controller.Index();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            dataService.Verify();

            var model = ((ViewResult)result).Model;
            model.Should().NotBeNull();
            model.Should().BeOfType<EmployeeViewModel>();            

            var dataViewModel = ((EmployeeViewModel)model);            
            dataViewModel.Data.Should().BeEquivalentTo(_dataServiceSampleData);            
        }       

        [Fact]
        public async Task EmployeeController_EmployeeList_Success()
        {
            // Arrange
            var logger = new Mock<ILogger<EmployeeController>>();                   
            var dataService = DataServiceMockExtensions.GetDataServceMock<Employee>().AddGet(_dataServiceSampleData);                  

            // Act 
            var controller = new EmployeeController(logger.Object, dataService.Object);
            var result = await controller.EmployeeList();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            dataService.Verify();

            var model = ((ViewResult)result).Model;
            model.Should().NotBeNull();
            model.Should().BeOfType<EmployeeViewModel>();            

            var dataViewModel = ((EmployeeViewModel)model);            
            dataViewModel.Data.Should().BeEquivalentTo(_dataServiceSampleData);            
        }        

        [Theory]        
        [ClassData(typeof(GetEmployeeTestCases))]
        public async Task EmployeeController_GetEmployee_Success(EmployeeViewModel testModel, string viewName, bool stateIsValid, bool success, string description)
        {
            _output.WriteLine("Description: {0}", description);

            // Arrange
            var logger = new Mock<ILogger<EmployeeController>>();                               
            var dataService = DataServiceMockExtensions.GetDataServceMock<Employee>();
            switch (testModel.Action)
            {
                case "updatedata":
                    dataService.AddPut(_dataServiceSampleData, success);  
                    break;
                case "insertdata":
                    dataService.AddPost(_dataServiceSampleData, success);  
                    break;
                case "edit":
                    dataService.AddGetById(_dataServiceSampleData, success);                    
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
            var controller = new EmployeeController(logger.Object, dataService.Object);
            if (!stateIsValid) controller.ModelState.AddModelError("EmployeeName", "Employee Name is Required");      
            var result = await controller.GetEmployee(testModel);            

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();            

            var viewResult = ((ViewResult)result);
            viewResult.ViewName.Should().BeEquivalentTo(viewName);

            var model = viewResult.Model;
            model.Should().NotBeNull();                        
            model.Should().BeOfType<EmployeeViewModel>();                        
        }                        
    }    

    
}