using Customers.Repositories.Abstraction.Contracts;
using Customers.Repositories.Context.Context;
using Customers.Repositories.Context.Entities;
using Customers.Repositories.Implementation.Concrete;
using Customers.Services.Abstraction.Contracts;
using Customers.Services.Implementation.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersApi.Extensions
{
    public class StartupExtensions
    {
        public static void ConfigureService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CustomersContext>(options => options.UseSqlServer(configuration.GetSection("Data").GetSection("CustomerDatabase").Value), ServiceLifetime.Transient);
            services.AddScoped<IRepository<CustomersModel>, CustomersRepository>();
            services.AddScoped<ICustomerService, CustomerService>();

        }
    }
}
