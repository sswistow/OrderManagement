using OrderManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagement.Infrastucture.Abstractions
{
    public interface IThreadRepository : IRepository<OrderThread>
    {
        Task<IEnumerable<OrderThread>> GetThreads();
        Task<OrderThread> UpdateThreadInterval(int id, int interval);
        OrderThread AddNew(OrderThread thread);
        OrderThread GetById(int id);
    }
}
