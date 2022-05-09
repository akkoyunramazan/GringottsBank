using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customer.Api.Entities
{
    public class CustomerModelEntityConfiguration
    {
        public static void Configure(EntityTypeBuilder<CustomerEntity> entityBuilder)
        {
            entityBuilder.HasKey(t => t.CustomerNumber);
            entityBuilder.Property(t => t.Name).IsRequired();
            entityBuilder.Property(t => t.Surname).IsRequired();
            entityBuilder.Property(t => t.Username).IsRequired();
            entityBuilder.Property(t => t.Password).IsRequired();
            entityBuilder.Property(t => t.Date).IsRequired();
        }
    }
}