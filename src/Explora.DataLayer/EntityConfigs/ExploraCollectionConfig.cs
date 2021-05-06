using Explora.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Explora.DataLayer.EntityConfigs
{
    public class ExploraCollectionConfig
    {
        public ExploraCollectionConfig(EntityTypeBuilder<ExploraCollection> entity)
        {
            entity.ToTable("collections");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).ValueGeneratedOnAdd().HasColumnName("id");
            entity.Property(p => p.Name).HasColumnName("name").IsRequired(false).HasMaxLength(100).HasDefaultValue(null);
            entity.Property(p => p.Description).HasColumnName("description").IsRequired(false).HasMaxLength(250).HasDefaultValue(null);
            entity.Property(p => p.ImageUrl).HasColumnName("image_url").IsRequired(false).HasMaxLength(255);
            entity.Property(p => p.LastTimeStamp).HasColumnName("last_timestamp").HasColumnType("timestamp").ValueGeneratedOnAddOrUpdate();
            entity.Property(p => p.ModifiedDate).HasColumnName("modified_date").IsRequired(false).HasColumnType("timestamp").ValueGeneratedOnUpdate();

            entity.HasMany(x => x.Files)
                .WithOne(e => e.Collection);
        }
    }
}
