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
            // Setup
            var configurationBuilder = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {                    
                    ["DAL:About"] = "http://mssqltest1.kaiser.guru/api/Version",
                    ["DAL:Customer"] = "http://mssqltest1.kaiser.guru/api/Customer",
                    ["DAL:Category"] = "http://mssqltest1.kaiser.guru/api/Category",
                    ["DAL:Employee"] = "http://mssqltest1.kaiser.guru/api/Employee",
                    ["DAL:Product"] = "http://mssqltest1.kaiser.guru/api/Product"
                });

            var configuration = configurationBuilder.Build();                                        

            // Act             
            var options = Microsoft.Extensions.Options.Options.Create(configuration.GetSection("DAL").Get<DALOptions>());            

            // Validate            
            options.Value.About.Should().BeEquivalentTo("http://mssqltest1.kaiser.guru/api/Version");
            options.Value.Category.Should().BeEquivalentTo("http://mssqltest1.kaiser.guru/api/Category");
            options.Value.Customer.Should().BeEquivalentTo("http://mssqltest1.kaiser.guru/api/Customer");
            options.Value.Employee.Should().BeEquivalentTo("http://mssqltest1.kaiser.guru/api/Employee");
            options.Value.Product.Should().BeEquivalentTo("http://mssqltest1.kaiser.guru/api/Product");
            options.Value.GetValue("Category").Should().BeEquivalentTo("http://mssqltest1.kaiser.guru/api/Category");
            options.Value.GetValue("Customer").Should().BeEquivalentTo("http://mssqltest1.kaiser.guru/api/Customer");
            options.Value.GetValue("Employee").Should().BeEquivalentTo("http://mssqltest1.kaiser.guru/api/Employee");
            options.Value.GetValue("Product").Should().BeEquivalentTo("http://mssqltest1.kaiser.guru/api/Product");
            options.Value.GetValue("").Should().BeEquivalentTo("");
        }
    }
}