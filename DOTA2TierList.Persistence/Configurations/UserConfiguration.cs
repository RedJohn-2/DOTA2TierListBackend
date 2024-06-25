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
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(u =>  u.Id);

            builder.HasAlternateKey(u => u.Email);

            builder.
                HasMany(u => u.Roles)
                .WithMany(r => r.Users);

            builder.
                HasMany(u => u.TierLists)
                .WithOne(tierList => tierList.User)
                .HasForeignKey(tierList => tierList.UserId);

            builder.Property(u => u.Name).HasMaxLength(80);
            builder.Property(u => u.Email).HasMaxLength(100);
            builder.Property(u => u.PasswordHash).HasMaxLength(100);
        }
    }
}
