using OrderManagement.Constants;
using OrderManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagement.Domain.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public OrderState State { get; set; }
        public int? ThreadId { get; set; }

        public OrderDTO(Order order)
        {
            Id = order.Id;
            CreationDate = order.CreationDate;
            State = order.State;
            ThreadId = order.Thread?.Id;
        }
    }
}
