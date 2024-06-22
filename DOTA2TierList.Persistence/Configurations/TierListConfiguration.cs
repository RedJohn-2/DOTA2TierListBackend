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
    public class TierListConfiguration : IEntityTypeConfiguration<TierListEntity>
    {
        public void Configure(EntityTypeBuilder<TierListEntity> builder)
        {
            builder.HasKey(tierList => tierList.Id);

            builder.
                HasOne(tierList => tierList.User)
                .WithMany(u => u.TierLists);

            builder.
                HasMany(tierList => tierList.Tiers)
                .WithOne(t => t.TierList)
                .HasForeignKey(t => t.TierListId);

            builder.
                HasOne(tierList => tierList.Type)
                .WithMany(tierType => tierType.TierLists)
                .HasForeignKey(tierType => tierType.TypeId);
        }
    }
}
