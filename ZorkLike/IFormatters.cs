using System;
using System.Collections.Generic;
using System.Linq;
using ZorkLike.Data;

namespace ZorkLike
{
    public interface IFormatter
    {
        void Format(GameObject go);
    }
}
