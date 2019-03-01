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
    public class AddThreadCommandHandlerTest
    {
        private readonly Mock<IThreadRepository> _threadRepositoryMock;
        private readonly Mock<IMediator> _mediator;

        public AddThreadCommandHandlerTest()
        {

            _threadRepositoryMock = new Mock<IThreadRepository>();
            _mediator = new Mock<IMediator>();
        }

        private OrderThread FakeThread()
        {
            return OrderThread.CreateNew(FakeCommand());
        }

        private AddThreadCommand FakeCommand()
        {
            return new AddThreadCommand { State = OrderThreadState.Stopped };
        }

        [Fact]
        public void AddThreadCommandHandler_success()
        {
            _threadRepositoryMock.Setup(threadRepo => threadRepo.AddNew(It.IsAny<OrderThread>()))
               .Returns(FakeThread());

            _threadRepositoryMock.Setup(threadRepo => threadRepo.UnitOfWork.SaveChangesAsync(default(CancellationToken)))
                .Returns(Task.FromResult(1));

            //Act
            var handler = new AddThreadCommandHandler(_threadRepositoryMock.Object);
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
