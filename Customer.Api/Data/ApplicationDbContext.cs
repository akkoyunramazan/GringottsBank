using Customer.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Customer.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            CustomerModelEntityConfiguration
                .Configure(modelBuilder.Entity<CustomerEntity>());

            base.OnModelCreating(modelBuilder);
        }
    }
}