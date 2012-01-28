using System;
using System.Collections.Generic;
using System.Linq;

namespace ZorkLike.Data
{
    public interface IRepositoryFactory : IDisposable
    {
        IRepository Create();
    }
}
