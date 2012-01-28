using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace ZorkLike.Data
{
    public class GameDbContext : DbContext
    {
        public DbSet<GameObject> GameObjects { get; set; }
    }
}
