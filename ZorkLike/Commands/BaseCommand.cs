using System;
using System.Collections.Generic;
using System.Linq;
using ZorkLike.Data;

namespace ZorkLike.Commands
{
    public abstract class BaseCommand : ICommand
    {
        protected readonly IConsoleFacade console;
        private readonly List<string> commandNames;
        protected readonly IFormatter[] formatters;

        public BaseCommand(IConsoleFacade console, IFormatter[] formatters)
        {
            this.console = console;
            this.commandNames = new List<string>();
            this.formatters = formatters;
        }

        protected void AddCommandName(string cmdName)
        {
            commandNames.Add(cmdName.ToLower());
        }

        public virtual bool IsValid(string cmd)
        {
            var cmdName = cmd.Split(' ')[0].ToLower();
            return commandNames.Any(m => m == cmdName);
        }
        public abstract void Execute(string cmd);
    }
}
