using Microsoft.EntityFrameworkCore;
using Transaction.Api.Data.Entities;

namespace Transaction.Api.Data
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

            AccountTransactionEntityConfiguration
                .Configure(modelBuilder.Entity<AccountTransactionEntity>());

            base.OnModelCreating(modelBuilder);
        }
        
        public DbSet<AccountTransactionEntity> Transactions { get; set; }
        
        public DbSet<AccountSummaryEntity> Accounts { get; set; }

    }
}