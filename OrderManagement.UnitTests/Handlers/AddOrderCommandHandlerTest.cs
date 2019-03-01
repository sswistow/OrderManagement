using MediatR;
using Moq;
using OrderManagement.Constants;
using OrderManagement.Controllers;
using OrderManagement.Domain.CommandHandlers;
using OrderManagement.Domain.Commands;
using OrderManagement.Domain.Models;
using OrderManagement.Infrastucture.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OrderManagement.UnitTests.Handlers
{
    public class AddOrderCommandHandlerTest
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<IMediator> _mediator;

        public AddOrderCommandHandlerTest()
        {

            _orderRepositoryMock = new Mock<IOrderRepository>();
            _mediator = new Mock<IMediator>();
        }

        private Order FakeOrder()
        {
            return Order.CreateNew(FakeCommand());
        }

        private AddOrderCommand FakeCommand()
        {
            return new AddOrderCommand { State = OrderState.Waiting };
        }

        [Fact]
        public void AddOrderCommandHandler_success()
        {
            _orderRepositoryMock.Setup(orderRepo => orderRepo.AddNew(It.IsAny<Order>()))
               .Returns(FakeOrder());

            _orderRepositoryMock.Setup(orderRepo => orderRepo.UnitOfWork.SaveChangesAsync(default(CancellationToken)))
                .Returns(Task.FromResult(1));

            //Act
            var handler = new AddOrderCommandHandler(_orderRepositoryMock.Object);
            var cltToken = new CancellationToken();

            Exception ex = null;

            try
            {
                var result = handler.Handle(FakeCommand(), cltToken);
            }
            catch (Exception e)
            {
                ex = e;
            }

            Assert.Null(ex);
        }
    }
}
