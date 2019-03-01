using Microsoft.Extensions.Configuration;
using OrderManagement.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagement.Configuration
{
    public class EnvironmentConfiguration : IEnvironmentConfiguration
    {
        private readonly IConfiguration _configuration;
        public EnvironmentConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string SqlConnectionString => _configuration.GetConnectionString(VariableNames.SqlConnectionString);
    }
}
