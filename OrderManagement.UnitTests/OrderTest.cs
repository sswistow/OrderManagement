using OrderManagement.Constants;
using OrderManagement.Domain.Commands;
using OrderManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OrderManagement.UnitTests
{
    public class OrderTest
    {
        public OrderTest() { }

        [Fact]
        public void Create_order_success()
        {
            //Arrange    
            var command = new AddOrderCommand { State = OrderState.Waiting };

            //Act 
            var order = Order.CreateNew(command);

            //Assert
            Assert.NotNull(order);
        }

        [Fact]
        public void Create_order_fail()
        {
            //Arrange    
            AddOrderCommand command = null;

            //Act - Assert
            Assert.Throws<ArgumentNullException>(() => Order.CreateNew(command));
        }

        [Fact]
        public void Finish_order_success()
        {
            //Arrange    
            var order = Order.CreateNew(new AddOrderCommand { State = OrderState.Waiting });

            //Act 
            order.Finish(OrderThread.CreateNew(new AddThreadCommand { State = OrderThreadState.Stopped }));

            //Assert
            Assert.NotNull(order);
        }

        [Fact]
        public void Finish_order_fail()
        {
            //Arrange    
            var order = Order.CreateNew(new AddOrderCommand { State = OrderState.Waiting });
            AddThreadCommand command = null;

            //Act - Assert
            Assert.Throws<ArgumentNullException>(() => order.Finish(OrderThread.CreateNew(command)));
        }
    }
}
