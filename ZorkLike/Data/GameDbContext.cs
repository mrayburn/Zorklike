using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace ZorkLike.Data
{
    public class GameDbContext : DbContext
    {
        public DbSet<GameObject> GameObjects { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameObject>().HasMany(m => m.Aliases).WithMany();
            modelBuilder.Entity<GameObject>().HasMany(m => m.Statuses).WithMany();
            base.OnModelCreating(modelBuilder);
        }
    }
}
