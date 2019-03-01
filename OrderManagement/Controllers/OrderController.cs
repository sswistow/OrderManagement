using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Constants;
using OrderManagement.Domain.Commands;
using OrderManagement.Domain.DTO;
using OrderManagement.Domain.Models;
using OrderManagement.Domain.Queries;

namespace OrderManagement.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<OrderDTO>> GetOrders()
        {
            var result = await _mediator.Send(new GetOrdersQuery());
            return result;
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<ThreadDTO>> GetThreads()
        {
            var result = await _mediator.Send(new GetThreadsQuery());
            return result;
        }

        [HttpPost("[action]")]
        public async Task<Order> AddOrder([FromBody]AddOrderCommand command)
        {
            var order = await _mediator.Send(command);
            return order;
        }

        [HttpPost("[action]")]
        public async Task<OrderThread> UpdateThreadInterval([FromBody]UpdateThreadIntervalCommand command)
        {
            var thread = await _mediator.Send(command);
            return thread;
        }

        [HttpPost("[action]")]
        public async Task<OrderThread> AddThread([FromBody]AddThreadCommand command)
        {
            var thread = await _mediator.Send(command);
            return thread;
        }

        [HttpPost("[action]")]
        public async Task<Order> ProcessOrder([FromBody]ProcessOrderCommand command)
        {
            Order order = null;

            var result = await Task.Factory.StartNew(async () =>
            {
                return await _mediator.Send(command);
            });

            order = await result;

            return order;
        }
    }
}