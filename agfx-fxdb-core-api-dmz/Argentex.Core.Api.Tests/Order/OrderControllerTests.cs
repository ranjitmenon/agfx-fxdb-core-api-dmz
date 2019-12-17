using Argentex.Core.Api.Controllers.Order;
using Argentex.Core.Service.Models.Order;
using Argentex.Core.Service.Order;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Argentex.Core.Api.Tests.Order
{
    public class OrderControllerTests
    {
        [Fact]
        public void ExecuteOrders_Success_With_Valid_Model_Input()
        {
            //Arrange
            var mockResponseList = new Mock<IList<OrderResponseModel>>();
            var mockService = new Mock<IOrderService>();
            mockService.Setup(x => x.ExecuteOrdersAsync(It.IsAny<OrderRequestModel>()))
                .Returns(Task.FromResult(mockResponseList.Object));

            var controller = new OrderController(mockService.Object);

            var orderModel = new OrderModel
            {
                ClientAmount = 1000,
                RhsCcy = "GBP",
                LhsCcy = "EUR",
                ValueDate = DateTime.Now
            };

            OrderRequestModel orderRequest = new OrderRequestModel
            {
                AuthUserId = 1,
                ClientCompanyId = 1,
                OrderModels = new List<OrderModel> { orderModel }
            };

            //Act
            var result = controller.ExecuteOrdersAsync(orderRequest).Result;

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void ExecuteOrders_Failed_When_InvalidModel_With_Bad_Request()
        {
            //Arrange
            var mockService = new Mock<IOrderService>();

            var controller = new OrderController(mockService.Object);
            controller.ModelState.AddModelError("", "Error");

            //Act
            var result = controller.ExecuteOrdersAsync(null).Result;

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void CanceOrder_Failed_When_Not_Passing_Code()
        {
            //Arrange
            var mockService = new Mock<IOrderService>();
            mockService.Setup(x => x.CancelOrderAsync("Code"))
            .Returns(Task.FromResult(true));

            var controller = new OrderController(mockService.Object);

            //Act
            var result = controller.CancelOrderAsync(null).Result;

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void CanceOrder_Success()
        {
            //Arrange
            var mockService = new Mock<IOrderService>();
            mockService.Setup(x => x.CancelOrderAsync("Code"))
                .Returns(Task.FromResult(true));

            var controller = new OrderController(mockService.Object);

            //Act
            var result = controller.CancelOrderAsync("Code").Result;

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
