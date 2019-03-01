using OrderManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagement.Infrastucture.Abstractions
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetAwaitingOrFinishedOrders();
        Order AddNew(Order order);
        Order GetById(int id);
        Order FinishOrder(Order order);
    }
}
