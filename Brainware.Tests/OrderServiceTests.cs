using System;
using Xunit;
using ASPNetCoreWeb.Infrastructure;
using ASPNetCoreWeb.Controllers;
using System.Linq;
using System.Collections;

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
            Assert.Contains(result, o => o.OrderId == 1 && 
                                        o.Description.Contains("Our first order") && 
                                        o.OrderProducts.Count == 3 && 
                                        o.OrderProducts.Exists(p => p.ProductId == 1) &&
                                        o.OrderProducts.Exists(p => p.ProductId == 3) &&
                                        o.OrderProducts.Exists(p => p.ProductId == 4) &&
                                        o.OrderTotal == 39.50M);
            Assert.Contains(result, o => o.OrderId == 2 && 
                                        o.Description.Contains("Our Second order") && 
                                        o.OrderProducts.Count == 4 &&
                                        o.OrderProducts.Exists(p => p.ProductId == 1) &&
                                        o.OrderProducts.Exists(p => p.ProductId == 2) &&
                                        o.OrderProducts.Exists(p => p.ProductId == 3) &&
                                        o.OrderProducts.Exists(p => p.ProductId == 5) &&
                                        o.OrderTotal == 44.00M);
            Assert.Contains(result, o => o.OrderId == 3 && 
                                        o.Description.Contains("Our third order") && 
                                        o.OrderProducts.Count == 5 &&
                                        o.OrderProducts.Exists(p => p.ProductId == 1) &&
                                        o.OrderProducts.Exists(p => p.ProductId == 2) &&
                                        o.OrderProducts.Exists(p => p.ProductId == 3) &&
                                        o.OrderProducts.Exists(p => p.ProductId == 4) &&
                                        o.OrderProducts.Exists(p => p.ProductId == 5) &&
                                        o.OrderTotal == 44.25M);
        }
        [Fact]
        public void OrdersControllerReturnsCorrectContents()
        {
            // Arrange
            var sut = new OrderController();
            var companyId = 1;

            // Act
            var result = sut.GetOrders(companyId);

            Assert.Equal(3, result.Count());
            Assert.Contains(result, o => o.OrderId == 1 &&
                                        o.Description.Contains("Our first order") &&
                                        o.OrderProducts.Count == 3 &&
                                        o.OrderProducts.Exists(p => p.ProductId == 1) &&
                                        o.OrderProducts.Exists(p => p.ProductId == 3) &&
                                        o.OrderProducts.Exists(p => p.ProductId == 4) &&
                                        o.OrderTotal == 39.50M);
            Assert.Contains(result, o => o.OrderId == 2 &&
                                        o.Description.Contains("Our Second order") &&
                                        o.OrderProducts.Count == 4 &&
                                        o.OrderProducts.Exists(p => p.ProductId == 1) &&
                                        o.OrderProducts.Exists(p => p.ProductId == 2) &&
                                        o.OrderProducts.Exists(p => p.ProductId == 3) &&
                                        o.OrderProducts.Exists(p => p.ProductId == 5) &&
                                        o.OrderTotal == 44.00M);
            Assert.Contains(result, o => o.OrderId == 3 &&
                                        o.Description.Contains("Our third order") &&
                                        o.OrderProducts.Count == 5 &&
                                        o.OrderProducts.Exists(p => p.ProductId == 1) &&
                                        o.OrderProducts.Exists(p => p.ProductId == 2) &&
                                        o.OrderProducts.Exists(p => p.ProductId == 3) &&
                                        o.OrderProducts.Exists(p => p.ProductId == 4) &&
                                        o.OrderProducts.Exists(p => p.ProductId == 5) &&
                                        o.OrderTotal == 44.25M);
        }
    }
}
