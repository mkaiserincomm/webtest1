using System;
using Xunit;
using Moq;
using Moq.Protected;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using FluentAssertions;
using webtest1.Controllers;
using webtest1.Services;
using webtest1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace webtest1.Tests
{
    public class AboutControllerTests
    {
        [Fact]
        public async Task AboutController_Index_Success()
        {
            const string resultString = "1234"; 
            const string testEnvironment = "TEST";  
            string versionTest = Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;

            // Arrange
            var logger = new Mock<ILogger<AboutController>>();                   
            var versionService = new Mock<IVersionService>();
            versionService
                .Setup(x => x.GetVersion())
                .ReturnsAsync(resultString)
                .Verifiable();

            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", testEnvironment);

            // Act
            var aboutController = new AboutController(logger.Object, versionService.Object);
            var result = await aboutController.Index();            

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            versionService.Verify();

            var model = ((ViewResult)result).Model;
            model.Should().NotBeNull();
            model.Should().BeOfType<AboutViewModel>();            

            var aboutViewModel = ((AboutViewModel)model);            
            aboutViewModel.DataVersion.Should().BeEquivalentTo(resultString);
            aboutViewModel.CurrentEnvironment.Should().BeEquivalentTo(testEnvironment);
            aboutViewModel.Version.Should().BeEquivalentTo(versionTest);                        
        }

        [Fact]
        public async Task AboutController_About_Success()
        {
            const string resultString = "1234"; 
            const string testEnvironment = "TEST";  
            string versionTest = Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;

            // Arrange
            var logger = new Mock<ILogger<AboutController>>();                   
            var versionService = new Mock<IVersionService>();
            versionService
                .Setup(x => x.GetVersion())
                .ReturnsAsync(resultString)
                .Verifiable();

            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", testEnvironment);

            // Act
            var aboutController = new AboutController(logger.Object, versionService.Object);
            var result = await aboutController.About();            

            // Assert
            result.Should().NotBeNull();                
            result.Should().BeOfType<ViewResult>();
            versionService.Verify();

            var model = ((ViewResult)result).Model;
            model.Should().NotBeNull();
            model.Should().BeOfType<AboutViewModel>();            

            var aboutViewModel = ((AboutViewModel)model);            
            aboutViewModel.DataVersion.Should().BeEquivalentTo(resultString);
            aboutViewModel.CurrentEnvironment.Should().BeEquivalentTo(testEnvironment);
            aboutViewModel.Version.Should().BeEquivalentTo(versionTest);                        
        }
    }
}