using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace ZorkLike.Data
{
    public interface IUnitOfWork
    {
        void SaveChanges();
        DbContext Context { get; }
    }
}
