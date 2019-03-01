using MediatR;
using Moq;
using OrderManagement.Constants;
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
    public class ProcessOrderCommandHandlerTest
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<IThreadRepository> _threadRepositoryMock;
        private readonly Mock<IMediator> _mediator;

        public ProcessOrderCommandHandlerTest()
        {
            _threadRepositoryMock = new Mock<IThreadRepository>();
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _mediator = new Mock<IMediator>();
        }

        private OrderThread FakeThread()
        {
            return OrderThread.CreateNew(FakeAddThreadCommand());
        }

        private AddThreadCommand FakeAddThreadCommand()
        {
            return new AddThreadCommand { State = OrderThreadState.Stopped };
        }

        private Order FakeOrder()
        {
            return Order.CreateNew(FakeAddOrderCommand());
        }

        private AddOrderCommand FakeAddOrderCommand()
        {
            return new AddOrderCommand { State = OrderState.Waiting };
        }

        private ProcessOrderCommand FakeCommand()
        {
            return new ProcessOrderCommand { OrderId = 1, ThreadId = 1 };
        }

        [Fact]
        public void ProcessOrderCommandHandlerTest_success()
        {
            _orderRepositoryMock.Setup(orderRepo => orderRepo.GetById(It.IsAny<int>()))
                .Returns(FakeOrder());

            _orderRepositoryMock.Setup(orderRepo => orderRepo.FinishOrder(It.IsAny<Order>()))
                .Returns(FakeOrder());

            _orderRepositoryMock.Setup(orderRepo => orderRepo.UnitOfWork.SaveChangesAsync(default(CancellationToken)))
                .Returns(Task.FromResult(1));

            _threadRepositoryMock.Setup(threadRepo => threadRepo.GetById(It.IsAny<int>()))
                .Returns(FakeThread());

            //Act
            var handler = new ProcessOrderCommandHandler(_orderRepositoryMock.Object, _threadRepositoryMock.Object);
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
