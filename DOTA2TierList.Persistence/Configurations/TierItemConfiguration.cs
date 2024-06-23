using DOTA2TierList.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.Persistence.Configurations
{
    public class TierItemConfiguration : IEntityTypeConfiguration<TierItemEntity>
    {
        public void Configure(EntityTypeBuilder<TierItemEntity> builder)
        {
            builder.HasKey(i => i.Id);

            builder.
                HasMany(i => i.Tiers)
                .WithMany(t => t.Items);

            builder.Property(i => i.Name).HasMaxLength(80);
        }
    }
}
