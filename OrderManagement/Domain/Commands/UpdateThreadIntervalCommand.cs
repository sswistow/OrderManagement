using MediatR;
using OrderManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagement.Domain.Commands
{
    public class UpdateThreadIntervalCommand : IRequest<OrderThread>
    {
        public int Interval { get; set; }
        public int ThreadId { get; set; }
    }
}
