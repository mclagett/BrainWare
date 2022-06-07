using System;
using Xunit;
using Web.Infrastructure;

namespace Brainware.Tests
{
    public class OrderServiceTests
    {
        [Fact]
        public void InitialDatabaseIntegrationTest()
        {
            // Arrange
            var sut = new OrderService();

            var companyId = 1;

            // Act
            var result = sut.GetOrdersForCompany(companyId);

            // Assert
            Assert.Equal(3, result.Count);
            Assert.Contains(result, o => o.OrderId == 1 && o.Description.Contains("Our first order") && o.OrderProducts.Count == 3 && o.OrderTotal == 39.50M);
            Assert.Contains(result, o => o.OrderId == 2 && o.Description.Contains("Our Second order") && o.OrderProducts.Count == 4 && o.OrderTotal == 44.00M);
            Assert.Contains(result, o => o.OrderId == 3 && o.Description.Contains("Our third order") && o.OrderProducts.Count == 5 && o.OrderTotal == 44.25M);
        }
    }
}
