using MediatR;
using Microsoft.EntityFrameworkCore;
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
    public class ProcessOrderCommandHandler : IRequestHandler<ProcessOrderCommand, Order>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IThreadRepository _threadRepository;

        public ProcessOrderCommandHandler(IOrderRepository orderRepository, IThreadRepository threadRepository)
        {
            _orderRepository = orderRepository;
            _threadRepository = threadRepository;
        }

        public async Task<Order> Handle(ProcessOrderCommand command, CancellationToken cancellationToken)
        {
            var order = _orderRepository.GetById(command.OrderId);
            var thread = _threadRepository.GetById(command.ThreadId);

            Random random = new Random();
            int mseconds = random.Next(1, 5) * 1000;
            Thread.Sleep(mseconds);

            order.Finish(thread);
            _orderRepository.FinishOrder(order);

            var saved = false;
            while (!saved)
            {
                try
                {
                    await _orderRepository.UnitOfWork.SaveEntitiesAsync();
                    saved = true;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    foreach (var entry in ex.Entries)
                    {
                        if (entry.Entity is Order)
                        {
                            var proposedValues = entry.CurrentValues;
                            var databaseValues = entry.GetDatabaseValues();

                            foreach (var property in proposedValues.Properties)
                            {
                                var proposedValue = proposedValues[property];
                                var databaseValue = databaseValues[property];

                                proposedValues[property] = databaseValue;
                            }

                            // Refresh original values to bypass next concurrency check
                            entry.OriginalValues.SetValues(databaseValues);

                            return entry.Entity as Order;
                        }
                        else
                        {
                            throw new NotSupportedException(
                                "Don't know how to handle concurrency conflicts for "
                                + entry.Metadata.Name);
                        }
                    }
                }
            }

            return order;
        }
    }
}
