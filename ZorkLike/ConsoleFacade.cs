using System;
using System.Collections.Generic;
using System.Linq;

namespace ZorkLike
{
    public class ConsoleFacade : IConsoleFacade
    {
        public void Write(string msg)
        {
            Console.Write(msg);
        }
        public string ReadLine()
        {
            return Console.ReadLine();
        }


        public void WriteLine(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
