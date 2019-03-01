using MediatR;
using OrderManagement.Configuration;
using OrderManagement.Domain.Commands;
using OrderManagement.Domain.Models;
using OrderManagement.Infrastucture.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrderManagement.Domain.CommandHandlers
{
    public class AddThreadCommandHandler : IRequestHandler<AddThreadCommand, OrderThread>
    {
        private readonly IThreadRepository _threadRepository;

        public AddThreadCommandHandler(IThreadRepository threadRepository)
        {
            _threadRepository = threadRepository;
        }
        public async Task<OrderThread> Handle(AddThreadCommand command, CancellationToken cancellationToken)
        {
            var thread = _threadRepository.AddNew(OrderThread.CreateNew(command));

            await _threadRepository.UnitOfWork.SaveEntitiesAsync();

            return thread;
        }
    }
}
