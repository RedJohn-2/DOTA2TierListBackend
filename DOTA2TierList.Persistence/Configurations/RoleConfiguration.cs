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
    public class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.HasKey(r => r.Id);

            builder.
                HasMany(r => r.Users)
                .WithMany(u => u.Roles);

            builder.Property(r => r.Name).HasMaxLength(80);

            builder.HasData(
                    new RoleEntity { Id = 1, Name = Enum.GetName(typeof(RoleEnum), (RoleEnum)1)! },
                    new RoleEntity { Id = 2, Name = Enum.GetName(typeof(RoleEnum), (RoleEnum)2)! },
                    new RoleEntity { Id = 3, Name = Enum.GetName(typeof(RoleEnum), (RoleEnum)3)! },
                    new RoleEntity { Id = 4, Name = Enum.GetName(typeof(RoleEnum), (RoleEnum)4)! }
                );
        }
    }
}
