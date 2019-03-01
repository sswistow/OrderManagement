using OrderManagement.Constants;
using OrderManagement.Domain.Commands;
using OrderManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OrderManagement.UnitTests
{
    public class OrderThreadTest
    {
        public OrderThreadTest() { }

        [Fact]
        public void Create_thread_success()
        {
            //Arrange    
            var command = new AddThreadCommand { State = OrderThreadState.Stopped };

            //Act 
            var thread = OrderThread.CreateNew(command);

            //Assert
            Assert.NotNull(thread);
        }

        [Fact]
        public void Create_thread_fail()
        {
            //Arrange    
            AddThreadCommand command = null;

            //Act - Assert
            Assert.Throws<ArgumentNullException>(() => OrderThread.CreateNew(command));
        }

        [Fact]
        public void Update_thread_interval_success()
        {
            //Arrange    
            var thread = OrderThread.CreateNew(new AddThreadCommand { State = OrderThreadState.Stopped });
            int interval = 100;
            //Act 
            thread.UpdateInterval(interval);

            //Assert
            Assert.NotNull(thread);
        }

        [Fact]
        public void Update_thread_interval_should_has_positive_value()
        {
            //Arrange    
            var thread = OrderThread.CreateNew(new AddThreadCommand { State = OrderThreadState.Stopped });
            int interval = -100;

            //Act - Assert
            Assert.Throws<ArgumentException>(() => thread.UpdateInterval(interval));
        }
    }
}
