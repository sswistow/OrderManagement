using OrderManagement.Configuration;
using OrderManagement.Infrastucture.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace OrderManagement.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDatabaseContext(this IServiceCollection services)
        {
            services.AddDbContext<OrderContext>((sp, opt) =>
            {
                var configuration = sp.GetService<IEnvironmentConfiguration>();
                opt.UseSqlServer(configuration.SqlConnectionString)
                    .EnableSensitiveDataLogging();
            });
        }
    }
}
