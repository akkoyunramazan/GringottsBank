using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Api.Entities
{
    public class UserModelEntityConfiguration
    {
        public static void Configure(EntityTypeBuilder<UserEntity> entityBuilder)
        {
            entityBuilder.HasKey(t => t.CustomerNumber);
            entityBuilder.Property(t => t.Name).IsRequired();
            entityBuilder.Property(t => t.Surname).IsRequired();
            entityBuilder.Property(t => t.Username).IsRequired();
            entityBuilder.Property(t => t.Password).IsRequired();
        }
    }
}