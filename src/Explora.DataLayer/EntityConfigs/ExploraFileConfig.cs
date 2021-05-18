using Explora.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Explora.DataLayer.EntityConfigs
{
    public class ExploraFileConfig
    {
        public ExploraFileConfig(EntityTypeBuilder<ExploraFile> entity)
        {
            entity.ToTable("files");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).ValueGeneratedOnAdd().HasColumnName("id");
            entity.Property(p => p.CollectionId).HasColumnName("collection_id").IsRequired(false);
            entity.Property(p => p.Name).HasColumnName("name").IsRequired(false).HasMaxLength(100).HasDefaultValue(null);
            entity.Property(p => p.ScientificName).HasColumnName("scientific_name").IsRequired(false).HasMaxLength(150).HasDefaultValue(null);
            entity.Property(p => p.Description).HasColumnName("description").IsRequired(false).HasMaxLength(250).HasDefaultValue(null);
            entity.Property(p => p.Url).HasColumnName("url").IsRequired(false).HasMaxLength(255);
            entity.Property(p => p.ImageUrl).HasColumnName("image_url").IsRequired(false).HasMaxLength(255);
            entity.Property(p => p.Deleted).HasColumnName("deleted");
            entity.Property(p => p.Extension).HasColumnName("extension").IsRequired(false);
            entity.Property(p => p.LastTimeStamp).HasColumnName("last_timestamp").HasColumnType("timestamp").ValueGeneratedOnAddOrUpdate();
            entity.Property(p => p.ModifiedDate).HasColumnName("modified_date").IsRequired(false).HasColumnType("timestamp").ValueGeneratedOnUpdate();
            entity.Property(p => p.Version).HasColumnName("version").IsRequired(true).HasDefaultValue(1);
            entity.Property(p => p.Platform).HasColumnName("platform").IsRequired(true).HasDefaultValue(1);

            entity.HasOne(e => e.Collection)
                .WithMany(c => c.Files)
                .HasForeignKey(e => e.CollectionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
