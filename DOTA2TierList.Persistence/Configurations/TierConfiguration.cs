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
    public class TierConfiguration : IEntityTypeConfiguration<TierEntity>
    {
        public void Configure(EntityTypeBuilder<TierEntity> builder)
        {
            builder.HasKey(t => t.Id);

            builder.
                HasOne(t => t.TierList)
                .WithMany(tierList => tierList.Tiers)
                .HasForeignKey(t => t.TierListId);

            builder.
                HasMany(t => t.Items)
                .WithMany(i => i.Tiers);

            builder.Property(t => t.Name).HasMaxLength(80);
        }
    }
}
