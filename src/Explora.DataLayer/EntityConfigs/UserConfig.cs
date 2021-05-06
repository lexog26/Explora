using Explora.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Explora.DataLayer.EntityConfigs
{
    public class UserConfig
    {
        public UserConfig(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("users");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).ValueGeneratedOnAdd();
            entity.Property(p => p.Name).HasColumnName("name").IsRequired(false).HasMaxLength(50).HasDefaultValue(null);
            entity.Property(p => p.LastName).HasColumnName("last_name").IsRequired(false).HasMaxLength(50).HasDefaultValue(null);
            entity.Property(p => p.SecondLastName).HasColumnName("second_last_name").IsRequired(false).HasMaxLength(50).HasDefaultValue(null);
            entity.Property(p => p.Email).HasColumnName("email").IsRequired(true).HasMaxLength(100);
            entity.Property(p => p.LastTimeStamp).HasColumnName("last_timestamp").HasColumnType("timestamp").ValueGeneratedOnAddOrUpdate();
        }
    }
}
