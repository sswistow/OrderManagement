using OrderManagement.Constants;
using OrderManagement.Domain.Commands;
using OrderManagement.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagement.Domain.Models
{
    public class Order : Entity, IAggregateRoot
    {
        protected Order(DateTime creationDate, OrderState state)
        {
            CreationDate = creationDate;
            State = state;
        }

        public DateTime CreationDate { get; set; }
        public OrderState State { get; set; }
        public OrderThread Thread { get; set; }

        public static Order CreateNew(AddOrderCommand newOrder)
        {
            if (newOrder == null) throw new ArgumentNullException(nameof(newOrder));

            return new Order(DateTime.UtcNow, newOrder.State);
        }

        public void Finish(OrderThread thread)
        {
            State = OrderState.Finished;
            Thread = thread ?? throw new ArgumentNullException(nameof(thread));
        }
    }
}
