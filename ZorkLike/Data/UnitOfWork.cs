using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Validation;

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
            try
            {
                dbContext.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var item in ex.EntityValidationErrors)
                {
                    foreach (var error in item.ValidationErrors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
            }
            finally
            {
                
            }
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
