using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DOTA2TierList.Logic.Models;
using DOTA2TierList.Persistence.Entities;
using DOTA2TierList.Logic.Models.TierItemTypes;
using DOTA2TierList.Persistence.Configurations;
using DOTA2TierList.Persistence.Entities.TierItemTypes;

namespace DOTA2TierList.Persistence
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<SteamProfileEntity> SteamProfiles { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<TierListEntity> TierLists { get; set; }
        public DbSet<TierEntity> Tiers { get; set; }
        public DbSet<TierItemEntity> TierItems { get; set; }
        public DbSet<HeroEntity> Heroes { get; set; }
        public DbSet<ArtifactEntity> Artifacts { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new TierConfiguration());
            modelBuilder.ApplyConfiguration(new TierListConfiguration());
            modelBuilder.ApplyConfiguration(new TierItemConfiguration());
            modelBuilder.ApplyConfiguration(new TierListTypeConfiguration());
        }

    }
}
