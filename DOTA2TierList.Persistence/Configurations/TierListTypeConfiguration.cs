using DOTA2TierList.Logic.Models.Enums;
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
    public class TierListTypeConfiguration : IEntityTypeConfiguration<TierListTypeEntity>
    {
        public void Configure(EntityTypeBuilder<TierListTypeEntity> builder)
        {
            builder.HasKey(tierType => tierType.Id);

            builder.
                HasMany(tierType => tierType.TierLists)
                .WithOne(tierList => tierList.Type);

            builder.Property(tierType => tierType.Name).HasMaxLength(80);

            builder.HasData(
                    new TierListTypeEntity { Id = 1, Name = Enum.GetName(typeof(TierListTypeEnum), (TierListTypeEnum)1)! },
                    new TierListTypeEntity { Id = 2, Name = Enum.GetName(typeof(TierListTypeEnum), (TierListTypeEnum)2)! }
                );
        }
    }
}
