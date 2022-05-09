using Identity.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            UserModelEntityConfiguration
                .Configure(modelBuilder.Entity<UserEntity>());

            base.OnModelCreating(modelBuilder);
        }
    }
}