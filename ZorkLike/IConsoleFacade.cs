using System;
using System.Collections.Generic;
using System.Linq;

namespace ZorkLike
{
    public interface IConsoleFacade
    {
        void Write(string msg);
        void WriteLine(string msg);
        string ReadLine();
    }
}
