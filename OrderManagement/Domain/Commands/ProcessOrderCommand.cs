using MediatR;
using OrderManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagement.Domain.Commands
{
    public class ProcessOrderCommand : IRequest<Order>
    {
        public int ThreadId { get; set; }
        public int OrderId { get; set; }
    }
}
