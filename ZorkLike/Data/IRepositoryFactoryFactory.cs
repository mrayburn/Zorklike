using System;
using System.Collections.Generic;
using System.Linq;

namespace ZorkLike.Data
{
    public interface IRepositoryFactoryFactory : IDisposable
    {
        IRepositoryFactory Create();
    }
}
