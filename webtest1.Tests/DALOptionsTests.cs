using Xunit;
using FluentAssertions;
using webtest1.Options;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace webtest1.Tests
{
    public class DALOptionsTests
    {
        [Fact]
        public void DALOptions_All_Success()
        {
            // Arrange
            var configurationBuilder = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {                                        
                    ["DAL:Customer"] = "http://customerservice.kaiser.guru/api/Customer",
                    ["DAL:Category"] = "http://categoryservice.kaiser.guru/api/Category",
                    ["DAL:Employee"] = "http://employeeservice.kaiser.guru/api/Employee",
                    ["DAL:Product"] = "http://productservice.kaiser.guru/api/Product",
                    ["DAL:CustomerAbout"] = "http://customerservice.kaiser.guru/api/Version",
                    ["DAL:CategoryAbout"] = "http://categoryservice.kaiser.guru/api/Version",
                    ["DAL:EmployeeAbout"] = "http://employeeservice.kaiser.guru/api/Version",
                    ["DAL:ProductAbout"] = "http://productservice.kaiser.guru/api/Version"
                });

            var configuration = configurationBuilder.Build();                                        

            // Act             
            var options = Microsoft.Extensions.Options.Options.Create(configuration.GetSection("DAL").Get<DALOptions>());            

            // Assert                        
            options.Value.Category.Should().BeEquivalentTo("http://categoryservice.kaiser.guru/api/Category");
            options.Value.Customer.Should().BeEquivalentTo("http://customerservice.kaiser.guru/api/Customer");
            options.Value.Employee.Should().BeEquivalentTo("http://employeeservice.kaiser.guru/api/Employee");
            options.Value.Product.Should().BeEquivalentTo("http://productservice.kaiser.guru/api/Product");
            options.Value.CategoryAbout.Should().BeEquivalentTo("http://categoryservice.kaiser.guru/api/Version");
            options.Value.CustomerAbout.Should().BeEquivalentTo("http://customerservice.kaiser.guru/api/Version");
            options.Value.EmployeeAbout.Should().BeEquivalentTo("http://employeeservice.kaiser.guru/api/Version");
            options.Value.ProductAbout.Should().BeEquivalentTo("http://productservice.kaiser.guru/api/Version");
            options.Value.GetValue("Category").Should().BeEquivalentTo("http://categoryservice.kaiser.guru/api/Category");
            options.Value.GetValue("Customer").Should().BeEquivalentTo("http://customerservice.kaiser.guru/api/Customer");
            options.Value.GetValue("Employee").Should().BeEquivalentTo("http://employeeservice.kaiser.guru/api/Employee");
            options.Value.GetValue("Product").Should().BeEquivalentTo("http://productservice.kaiser.guru/api/Product");            
            options.Value.GetValue("").Should().BeEquivalentTo("");
        }
    }
}