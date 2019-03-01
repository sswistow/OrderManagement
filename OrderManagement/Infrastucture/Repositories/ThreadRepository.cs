using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Models;
using OrderManagement.Infrastucture.Abstractions;
using OrderManagement.Infrastucture.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagement.Infrastucture.Repositories
{
    public class ThreadRepository : IThreadRepository
    {
        private readonly OrderContext _context;

        public ThreadRepository(OrderContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<OrderThread> UpdateThreadInterval(int id, int interval)
        {
            var thread = await _context.Threads.FirstOrDefaultAsync(t => t.Id == id);
            thread.UpdateInterval(interval);
            _context.Threads.Update(thread);

            return thread;
        }

        public async Task<IEnumerable<OrderThread>> GetThreads()
        {
            var result = await _context.Threads.ToListAsync();
            return result;
        }

        public OrderThread AddNew(OrderThread thread)
        {
            return _context.Threads.Add(thread).Entity;
        }

        public OrderThread GetById(int id)
        {
            return _context.Threads.FirstOrDefault(o => o.Id == id);
        }
    }
}
