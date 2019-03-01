using MediatR;
using OrderManagement.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagement.Domain.Queries
{
    public class GetThreadsQuery : IRequest<IEnumerable<ThreadDTO>> { }
}
