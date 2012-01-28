using System;
using System.Collections.Generic;
using System.Linq;
using ZorkLike.Data;

namespace ZorkLike.Commands
{
    public class LookCommand : ICommand
    {
        private readonly IRepositoryFactoryFactory factory;
        private readonly IConsoleFacade console;
        private readonly IGameObjectQueries goQueries;
        public LookCommand(IConsoleFacade console, IRepositoryFactoryFactory factory, IGameObjectQueries goQueries)
        {
            this.console = console;
            this.factory = factory;
            this.goQueries = goQueries;
        }
        public bool IsValid(string cmd)
        {
            return cmd.Split(' ')[0].ToLower() == "look";
        }
        public void Execute(string cmd)
        {
            using (var fac = factory.Create())
            {
                var goName = cmd.Split(' ')[1];
                var repo = fac.Create();
                var player = goQueries.GetPlayer(repo);
                var go = goQueries.GetGameObjectByNameAndLocation(repo, goName, player.Location);
                if (go == null)
                {
                    console.WriteLine("You can't find anything like that around.");
                }
                else console.WriteLine(go.Description ?? "You see nothing special.");
            }
        }

    }
}
