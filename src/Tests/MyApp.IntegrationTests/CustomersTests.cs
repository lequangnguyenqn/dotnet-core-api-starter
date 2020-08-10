using MyApp.Application.Customers.Add;
using MyApp.IntegrationTests.Configuration;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyApp.IntegrationTests
{
    public class CustomersTests : TestBase
    {
        [Fact]
        public async Task Add_Customer_Return_Ok()
        {
            var client = await CreateClient();
            var addCustomerCommand = new StringContent(JsonConvert.SerializeObject(new AddCustomerCommand
            {
                Email = "terry.le@example.com",
                Name = "Terry Le"
            }
            ), Encoding.UTF8, "application/json");

            var addCustomerResponse = await client.PostAsync("/api/customers", addCustomerCommand);

            var jsonString = await addCustomerResponse.Content.ReadAsStringAsync();
            var customer = JsonConvert.DeserializeObject<AddCustomerRespone>(jsonString);
            var getCustomerResponse = await client.GetAsync($"/api/customers/{customer.CustomerId}");
            var getCustomersResponse = await client.GetAsync("/api/customers");

            Assert.Equal(HttpStatusCode.Created, addCustomerResponse.StatusCode);
            Assert.Equal(HttpStatusCode.OK, getCustomerResponse.StatusCode);
            Assert.Equal(HttpStatusCode.OK, getCustomersResponse.StatusCode);
        }

        [Theory]
        [InlineData("invalid_email", "Terry Le")]
        [InlineData("example.com", "Terry Le")]
        [InlineData("a@@example.com", "Terry Le")]
        [InlineData("a b@@example.com", "Terry Le")]
        [InlineData("terry.le@example.com", "Name must be less than 100 characters. Name must be less than 100 characters. Name must be less than 100 characters.")]
        public async Task Add_Customer_Return_BadRequest(string email, string name)
        {
            // Arrange
            var client = await CreateClient();
            var addCustomerCommand = new StringContent(JsonConvert.SerializeObject(new AddCustomerCommand
            {
                Email = email,
                Name = name
            }
            ), Encoding.UTF8, "application/json");

            // Act
            var addCustomerResponse = await client.PostAsync("/api/customers", addCustomerCommand);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, addCustomerResponse.StatusCode);
        }

        [Fact]
        public async Task Add_Duplicate_Customer_Return_BadRequest()
        {
            // Arrange
            var client = await CreateClient();
            var addCustomerCommand = new StringContent(JsonConvert.SerializeObject(new AddCustomerCommand
            {
                Email = "terry.le@example.com",
                Name = "Terry Le"
            }), Encoding.UTF8, "application/json");

            // Act
            var addCustomerResponseFirstTime = await client.PostAsync("/api/customers", addCustomerCommand);
            var addCustomerResponseSecondTIme = await client.PostAsync("/api/customers", addCustomerCommand);

            // Assert
            Assert.Equal(HttpStatusCode.Created, addCustomerResponseFirstTime.StatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, addCustomerResponseSecondTIme.StatusCode);
        }

        [Fact]
        public async Task Get_Customer_Return_BadRequest()
        {
            // Arrange
            var client = await CreateClient();

            // Act
            var getCustomerResponse = await client.GetAsync($"/api/customers/06b748ca-7ad1-429a-a1f1-5595591450c2");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, getCustomerResponse.StatusCode);
        }
    }
}
