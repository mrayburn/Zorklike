using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace ZorkLike.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private DbContext dbContext;
        /// <summary>
        /// Initializes a new instance of the UnitOfWork class.
        /// </summary>
        /// <param name="dbContext"></param>
        public UnitOfWork(DbContext dbContext)
        {
            this.dbContext = dbContext;
            Database.SetInitializer<GameDbContext>(new DropCreateDatabaseIfModelChanges<GameDbContext>());
        }
        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }


        public DbContext Context
        {
            get { return dbContext; }
        }
    }
}
