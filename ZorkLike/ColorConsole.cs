using System;
using System.Collections.Generic;
using System.Linq;

namespace ZorkLike
{
    public class ColorConsole : IConsoleFacade
    {

        public void Write(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(msg);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public string ReadLine()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            var input = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            return input;
        }


        public void WriteLine(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
