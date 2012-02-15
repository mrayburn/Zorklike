using System;
using System.Collections.Generic;
using System.Linq;
using ZorkLike.Data;

namespace ZorkLike.Commands
{
    public class PutDownCommand : BaseDataCommand
    {
        public PutDownCommand(IConsoleFacade console, IRepositoryFactoryFactory factory, IGameObjectQueries goQueries, IFormatter[] formatters)
            : base(console, factory, goQueries, formatters)
        {
            AddCommandName("putdown");
            AddCommandName("drop");
        }
        protected override bool ExecuteWithData(string cmd, IRepository repo, Player player)
        {
            var goName = cmd.Split(' ')[1];
            var go = goQueries.GetGameObjectByNameAndLocation(repo, goName, player);
            if (go == null)
            {
                console.WriteLine("You don't see anything like that around");
                return false;
            }
            else
            {
                go.Location = player.Location;
                console.Write("You are no longer carrying: ");
                console.WriteLine(goName);
                return true;
            }

        }
    }
}
