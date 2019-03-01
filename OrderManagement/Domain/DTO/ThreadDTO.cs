using OrderManagement.Constants;
using OrderManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagement.Domain.DTO
{
    public class ThreadDTO
    {
        public int Id { get; set; }
        public int Interval { get; set; }
        public OrderThreadState State { get; set; }

        public ThreadDTO(OrderThread thread)
        {
            Id = thread.Id;
            Interval = thread.Interval;
            State = thread.State;
        }
    }
}
