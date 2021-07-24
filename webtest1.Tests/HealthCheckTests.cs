using Xunit;
using FluentAssertions;
using webtest1.Tests.TestData;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading.Tasks;

namespace webtest1.Tests
{
    public class HealthCheckTests
    {
        [Theory]
        [ClassData(typeof(HealthCheckTestData))]
        public async Task LiveCheck_CheckHealthAsync_Success(IHealthCheck healthCheck)
        {
            // Setup
            var healthCheckContext = new HealthCheckContext();

            // Act             
            var result = await healthCheck.CheckHealthAsync(healthCheckContext);

            // Validate
            result.Should().NotBeNull();
            (result.Status == HealthStatus.Healthy).Should().BeTrue();
        }
    }
}