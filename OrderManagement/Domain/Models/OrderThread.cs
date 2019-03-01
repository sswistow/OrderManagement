using OrderManagement.Constants;
using OrderManagement.Domain.Commands;
using OrderManagement.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagement.Domain.Models
{
    public class OrderThread : Entity, IAggregateRoot
    {
        public const int InitInterval = 1000;
        protected OrderThread(OrderThreadState state)
        {
            State = state;
            Interval = InitInterval;
        }

        public int Interval { get; set; }
        public OrderThreadState State { get; set; }


        public void UpdateInterval(int interval)
        {
            if (interval <= 0) throw new ArgumentException(nameof(interval));
            Interval = interval;
        }

        public static OrderThread CreateNew(AddThreadCommand newThread)
        {
            if (newThread == null) throw new ArgumentNullException(nameof(newThread));

            return new OrderThread(newThread.State);
        }
    }
}
