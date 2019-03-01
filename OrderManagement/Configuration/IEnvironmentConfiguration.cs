using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagement.Configuration
{
    public interface IEnvironmentConfiguration
    {
        string SqlConnectionString { get; }
    }
}
