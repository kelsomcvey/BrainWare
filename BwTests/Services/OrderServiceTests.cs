using Api.Controllers;
using Api.Infrastructure.Services;
using Api.Models;
using DataRepository.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BwTests.Services
{
    public class OrderServiceTests
    {
        [Fact]
        public async Task GetOrdersWithProducts_ThrowsException_WhenErrorOccurs()
        {
            // Arrange
            var companyId = 1;
            var mockRepository = new Mock<IBrainWareRepository>();
            var mockOrderServiceLogger = new Mock<ILogger<OrderService>>();
            mockRepository.Setup(repo => repo.ExecuteStoredProcedure<Order>(It.IsAny<string>(), It.IsAny<object>()))
                          .ThrowsAsync(new Exception("Error executing stored procedure"));
            var orderService = new OrderService(mockRepository.Object, mockOrderServiceLogger.Object);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await orderService.GetOrdersWithProducts(companyId);
            });
        }

        // Add more test methods as needed
    
    [Fact]
        public async Task GetOrdersWithProducts_ReturnsListOfOrders_WhenCompanyIdIsValid()
        {
            // Arrange
            var companyId = 1;
            var mockRepository = new Mock<IBrainWareRepository>();
            var mockOrderServiceLogger = new Mock<ILogger<OrderService>>();
            var orders = new List<Order>
            {
                new Order { OrderId = 1, Description = "Order 1" },
                new Order { OrderId = 2, Description = "Order 2" }
            };
            var orderProducts = new List<OrderProduct>
            {
                new OrderProduct { OrderId = 1, ProductId = 1 },
                new OrderProduct { OrderId = 1, ProductId = 2 },
                new OrderProduct { OrderId = 2, ProductId = 3 }
            };
            mockRepository.Setup(repo => repo.ExecuteStoredProcedure<Order>(It.IsAny<string>(), It.IsAny<object>()))
                          .ReturnsAsync(orders);
            mockRepository.Setup(repo => repo.ExecuteStoredProcedure<OrderProduct>(It.IsAny<string>(), It.IsAny<object>()))
                          .ReturnsAsync(orderProducts);
            var orderService = new OrderService(mockRepository.Object, mockOrderServiceLogger.Object);

            // Act
            var result = await orderService.GetOrdersWithProducts(companyId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Order>>(result);
            Assert.Equal(orders.Count, result.Count);

            foreach (var order in result)
            {
                Assert.NotNull(order.OrderProducts);
                Assert.NotEmpty(order.OrderProducts);
            }
        }

        // Add more test methods as needed
    }
}
