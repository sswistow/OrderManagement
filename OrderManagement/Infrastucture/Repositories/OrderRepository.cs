using Microsoft.EntityFrameworkCore;
using OrderManagement.Constants;
using OrderManagement.Domain.Models;
using OrderManagement.Infrastucture.Abstractions;
using OrderManagement.Infrastucture.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagement.Infrastucture.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderContext _context;

        public OrderRepository(OrderContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => _context;

        public Order AddNew(Order order)
        {
            return _context.Orders.Add(order).Entity;
        }

        public Order FinishOrder(Order order)
        {
            _context.Orders.Update(order);
            return order;
        }

        public async Task<IEnumerable<Order>> GetAwaitingOrFinishedOrders()
        {
            var result = await _context.Orders
                .Include(o => o.Thread)
                .Where(o => o.State == OrderState.Waiting || o.State == OrderState.Finished)
                .ToListAsync();
            return result;
        }

        public Order GetById(int id)
        {
            return _context.Orders.FirstOrDefault(o => o.Id == id);
        }
    }
}
