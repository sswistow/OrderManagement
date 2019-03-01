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
    public class UpdateThreadIntervalCommandHandler : IRequestHandler<UpdateThreadIntervalCommand, OrderThread>
    {
        private readonly IThreadRepository _threadRepository;

        public UpdateThreadIntervalCommandHandler(IThreadRepository threadRepository)
        {
            _threadRepository = threadRepository;
        }

        public async Task<OrderThread> Handle(UpdateThreadIntervalCommand command, CancellationToken cancellationToken)
        {
            var thread = await _threadRepository.UpdateThreadInterval(command.ThreadId, command.Interval);

            await _threadRepository.UnitOfWork.SaveEntitiesAsync();

            return thread;
        }
    }
}
