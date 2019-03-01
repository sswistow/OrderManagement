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
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IEnumerable<OrderDTO>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrdersQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<OrderDTO>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAwaitingOrFinishedOrders();
            var result = orders.Select(x => new OrderDTO(x));

            return result;
        }
    }
}
