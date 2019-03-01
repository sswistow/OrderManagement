using MediatR;
using OrderManagement.Configuration;
using OrderManagement.Domain.DTO;
using OrderManagement.Domain.Queries;
using OrderManagement.Infrastucture.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrderManagement.Domain.QueryHandlers
{
    public class GetThreadsQueryHandler : IRequestHandler<GetThreadsQuery, IEnumerable<ThreadDTO>>
    {
        private readonly IThreadRepository _threadRepository;

        public GetThreadsQueryHandler(IThreadRepository threadRepository)
        {
            _threadRepository = threadRepository;
        }

        public async Task<IEnumerable<ThreadDTO>> Handle(GetThreadsQuery request, CancellationToken cancellationToken)
        {
            var threads = await _threadRepository.GetThreads();
            var result = threads.Select(x => new ThreadDTO(x));

            return result;
        }
    }
}
