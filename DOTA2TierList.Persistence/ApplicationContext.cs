using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DOTA2TierList.Logic.Models;
using DOTA2TierList.Persistence.Entities;
using DOTA2TierList.Logic.Models.TierItemTypes;

namespace DOTA2TierList.Persistence
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<TierListEntity> TierLists { get; set; }
        public DbSet<TierEntity> Tiers { get; set; }
        public DbSet<TierItemEntity> TierListItems { get; set; }
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<Artifact> Artifacts { get; set; }

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
            modelBuilder.Entity<TierItem>().UseTpcMappingStrategy();
        }

    }
}
