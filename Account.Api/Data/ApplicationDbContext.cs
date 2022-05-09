using Account.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Account.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AccountSummaryEntityConfiguration
                .Configure(modelBuilder.Entity<AccountSummaryEntity>());

            base.OnModelCreating(modelBuilder);
            
        }
        
        public DbSet<AccountSummaryEntity> Accounts { get; set; }
    }
}