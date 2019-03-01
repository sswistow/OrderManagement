using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagement.Constants
{
    public enum OrderState
    {
        Waiting, Working, Finished
    }

    public enum OrderThreadState
    {
        Working, Stopped
    }
}
