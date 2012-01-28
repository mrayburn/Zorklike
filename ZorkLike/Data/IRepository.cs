using System;
using System.Collections.Generic;
using System.Linq;

namespace ZorkLike.Data
{
    public interface IRepository
    {
        IUnitOfWork UnitOfWork { get; }
        IQueryable<T> AsQueryable<T>() where T : class;
        T Add<T>(T obj) where T : class;
        void Delete<T>(T obj) where T : class;
    }
}
