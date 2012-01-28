using System;
using System.Collections.Generic;
using System.Linq;

namespace ZorkLike
{
    public class GameEngine
    {
        private IConsoleFacade console;
        private ICommand[] cmds;
        public GameEngine(IConsoleFacade console, params ICommand[] cmds)
        {
            this.cmds = cmds;
            this.console = console;
        }
        public void Run() 
        {
            var input = "";
            do
            {
                console.Write("> ");
                input = console.ReadLine();

                foreach (ICommand command in cmds)
                {
                    bool isValid = command.IsValid(input);
                    if (isValid)
                    {
                        command.Execute(input);
                        break;
                    }
                }
                
            } while (!input.Trim().Equals("exit", StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
