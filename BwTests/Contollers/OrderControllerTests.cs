using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Controllers;
using Api.Infrastructure.Services;
using Api.Models;
using DataRepository.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace BwTests.Contollers
{
    public class OrderControllerTests
    {
        [Fact]
        public async Task GetOrderCompanies_ReturnsOkResult_WithListOfCompanies()
        {
            // Arrange
            var mockOrderService = new Mock<IOrderService>();
            var mockOrderControllerLogger = new Mock<ILogger<OrderController>>();
            var companies = new List<Company>
            {
                new Company { CompanyId = 1, CompanyName = "Company A" },
                new Company { CompanyId = 2, CompanyName = "Company B" }
            };
            mockOrderService.Setup(service => service.GetCompanies()).ReturnsAsync(companies);
            var controller = new OrderController(mockOrderService.Object, mockOrderControllerLogger.Object);

            // Act
            var result = await controller.GetOrderCompanies();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<Company>>(okResult.Value);
            Assert.Equal(companies, model);
        }

        [Fact]
        public async Task GetOrders_ReturnsOkResult_WithListOfOrders()
        {
            // Arrange
            var mockOrderService = new Mock<IOrderService>();
            var mockOrderControllerLogger = new Mock<ILogger<OrderController>>();
            var orders = new List<Order>
            {
                new Order { OrderId = 1, Description = "Order 1" },
                new Order { OrderId = 2, Description = "Order 2" }
            };
            mockOrderService.Setup(service => service.GetOrdersWithProducts(It.IsAny<int>())).ReturnsAsync(orders);
            var controller = new OrderController(mockOrderService.Object, mockOrderControllerLogger.Object);

            // Act
            var result = await controller.GetOrders(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<Order>>(okResult.Value);
            Assert.Equal(orders, model);
        }
    }
}
