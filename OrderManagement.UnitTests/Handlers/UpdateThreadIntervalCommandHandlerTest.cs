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
    public class UpdateThreadIntervalCommandHandlerTest
    {
        private readonly Mock<IThreadRepository> _threadRepositoryMock;
        private readonly Mock<IMediator> _mediator;

        public UpdateThreadIntervalCommandHandlerTest()
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

        private UpdateThreadIntervalCommand FakeUpdateIntervalCommand()
        {
            return new UpdateThreadIntervalCommand { Interval = 100, ThreadId = 1 };
        }

        [Fact]
        public void AddThreadCommandHandler_success()
        {
            _threadRepositoryMock.Setup(threadRepo => threadRepo.UpdateThreadInterval(It.IsAny<int>(), It.IsAny<int>()))
               .Returns(Task.FromResult(FakeThread()));

            _threadRepositoryMock.Setup(threadRepo => threadRepo.UnitOfWork.SaveChangesAsync(default(CancellationToken)))
                .Returns(Task.FromResult(1));

            //Act
            var handler = new UpdateThreadIntervalCommandHandler(_threadRepositoryMock.Object);
            var cltToken = new CancellationToken();

            Exception ex = null;

            try
            {
                var result = handler.Handle(FakeUpdateIntervalCommand(), cltToken);
            }
            catch (Exception e)
            {
                ex = e;
            }

            Assert.Null(ex);
        }
    }
}
