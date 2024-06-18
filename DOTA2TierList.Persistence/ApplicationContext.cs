using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DOTA2TierList.Logic.Models;
using DOTA2TierList.Logic.Models.TierListModel;

namespace DOTA2TierList.Persistence
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<TierList> TierLists { get; set; }
        public DbSet<Tier> Tiers { get; set; }
        public DbSet<TierListItem> TierListItems { get; set; }
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<Artifact> Artifacts { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TierListItem>().UseTpcMappingStrategy();
        }

    }
}
