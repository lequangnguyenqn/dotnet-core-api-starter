using MyApp.IntegrationTests.Configuration;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace MyApp.IntegrationTests
{
    public class CustomersTests : TestBase
    {
        [Fact]
        public async Task Get_Customer_Return_Ok()
        {
            // Arrange
            var client = await CreateClient();

            // Act
            var response = await client.GetAsync("/api/customers");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
