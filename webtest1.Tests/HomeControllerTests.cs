using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using FluentAssertions;
using webtest1.Controllers;
using webtest1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace webtest1.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void HomeController_Index_Success()
        {
            // Setup
            var logger = new Mock<ILogger<HomeController>>();                                                                

            // Act 
            var controller = new HomeController(logger.Object);
            var result = controller.Index();

            // Validate
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();            

            var viewResult = ((ViewResult)result);
            viewResult.ViewName.Should().BeNull();            
        }       

        [Fact]
        public void HomeController_Error_Success()
        {
            // Setup
            var logger = new Mock<ILogger<HomeController>>();    
            
            // Act 
            var controller = new HomeController(logger.Object);
            controller.ControllerContext = new ControllerContext{
                HttpContext = new DefaultHttpContext()
            };
            var result = controller.Error();

            // Validate
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();            

            var viewResult = ((ViewResult)result);
            viewResult.ViewName.Should().BeNull();    

            var model = viewResult.Model;
            model.Should().NotBeNull();                        
            model.Should().BeOfType<ErrorViewModel>();                
            ((ErrorViewModel)model).ShowRequestId.Should().BeTrue();

            ((ErrorViewModel)model).RequestId = "";
            ((ErrorViewModel)model).ShowRequestId.Should().BeFalse();

        }       

    }    
}