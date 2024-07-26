using DOTA2TierList.Logic.Models;
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
    public class SteamProfileConfiguration : IEntityTypeConfiguration<SteamProfileEntity>
    {
        public void Configure(EntityTypeBuilder<SteamProfileEntity> builder)
        {
            builder.HasKey(p =>  p.Id);

            builder.
                HasOne(p => p.User)
                .WithOne(u => u.SteamProfile);


        }
    }
}
