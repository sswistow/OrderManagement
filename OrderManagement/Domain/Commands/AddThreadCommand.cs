using MediatR;
using OrderManagement.Constants;
using OrderManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagement.Domain.Commands
{
    public class AddThreadCommand : IRequest<OrderThread>
    {
        public OrderThreadState State { get; set; }
    }
}
