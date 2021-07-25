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
    public class CustomerControllerTests
    {
        private readonly ITestOutputHelper _output;        
        private readonly List<Customer> _dataServiceSampleData;

        public CustomerControllerTests(ITestOutputHelper output)
        {
            _output = output;    
            _dataServiceSampleData = GetCustomerTestData.TestData;
        }

        [Fact]
        public async Task CustomerController_Index_Success()
        {
            // Arrange
            var logger = new Mock<ILogger<CustomerController>>();                   
            var dataService = DataServiceMockExtensions.GetDataServceMock<Customer>().AddGet(_dataServiceSampleData);                  

            // Act 
            var controller = new CustomerController(logger.Object, dataService.Object);
            var result = await controller.Index();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            dataService.Verify();

            var model = ((ViewResult)result).Model;
            model.Should().NotBeNull();
            model.Should().BeOfType<DataViewModel<Customer>>();            

            var dataViewModel = ((DataViewModel<Customer>)model);            
            dataViewModel.Data.Should().BeEquivalentTo(_dataServiceSampleData);            
        }       

        [Fact]
        public async Task CustomerController_CustomerList_Success()
        {
            // Arrange
            var logger = new Mock<ILogger<CustomerController>>();                   
            var dataService = DataServiceMockExtensions.GetDataServceMock<Customer>().AddGet(_dataServiceSampleData);                  

            // Act 
            var controller = new CustomerController(logger.Object, dataService.Object);
            var result = await controller.CustomerList();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            dataService.Verify();

            var model = ((ViewResult)result).Model;
            model.Should().NotBeNull();
            model.Should().BeOfType<DataViewModel<Customer>>();            

            var dataViewModel = ((DataViewModel<Customer>)model);            
            dataViewModel.Data.Should().BeEquivalentTo(_dataServiceSampleData);            
        }        

        [Theory]        
        [ClassData(typeof(GetCustomerTestCases))]
        public async Task CustomerController_GetCustomer_Success(DataViewModel<Customer> testModel, string viewName, bool stateIsValid, bool success, string description)
        {
            _output.WriteLine("Description: {0}", description);

            // Arrange
            var logger = new Mock<ILogger<CustomerController>>();                               
            var dataService = DataServiceMockExtensions.GetDataServceMock<Customer>();
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
            var controller = new CustomerController(logger.Object, dataService.Object);
            if (!stateIsValid) controller.ModelState.AddModelError("CustomerName", "Customer Name is Required");      
            var result = await controller.GetCustomer(testModel);            

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();            

            var viewResult = ((ViewResult)result);
            viewResult.ViewName.Should().BeEquivalentTo(viewName);

            var model = viewResult.Model;
            model.Should().NotBeNull();                        
            model.Should().BeOfType<DataViewModel<Customer>>();                        
        }                        
    }    

    
}