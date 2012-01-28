using System;
using System.Collections.Generic;
using System.Linq;

namespace ZorkLike.Data
{
    public class Repository : IRepository
    {
        private IUnitOfWork unitOfWork;
        /// <summary>
        /// Initializes a new instance of the Repository class.
        /// </summary>
        /// <param name="unitOfWork"></param>
        public Repository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IQueryable<T> AsQueryable<T>() where T : class
        {
            return unitOfWork.Context.Set<T>();
        }

        public T Add<T>(T obj) where T : class
        {
            return unitOfWork.Context.Set<T>().Add(obj);
        }

        public void Delete<T>(T obj) where T : class
        {
            unitOfWork.Context.Set<T>().Remove(obj);
        }

        public IUnitOfWork UnitOfWork
        {
            get { return unitOfWork; }
        }
    }
}
