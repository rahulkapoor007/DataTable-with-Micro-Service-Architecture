
using Customers.Repositories.Context.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Customers.Repositories.Context.Context
{
    public class CustomersContext : DbContext
    {
        private IConfiguration _config;
        public CustomersContext(DbContextOptions<CustomersContext> options, IConfiguration config)
            : base(options)
        {
            _config = config;
        }
        public DbSet<CustomersModel> Customers { get; set; }
    }
}
