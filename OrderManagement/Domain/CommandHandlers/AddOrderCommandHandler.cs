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
    public class AddOrderCommandHandler : IRequestHandler<AddOrderCommand, Order>
    {
        private readonly IOrderRepository _orderRepository;

        public AddOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Order> Handle(AddOrderCommand command, CancellationToken cancellationToken)
        {
            var order = _orderRepository.AddNew(Order.CreateNew(command));

            await _orderRepository.UnitOfWork.SaveEntitiesAsync();

            return order;
        }
    }
}
