using System;
using System.Collections.Generic;
using System.Linq;
using ZorkLike.Data;

namespace ZorkLike.Commands
{
    public class MoveCommand : BaseDataCommand
    {

        public MoveCommand(IConsoleFacade console, IRepositoryFactoryFactory factory, IGameObjectQueries goQueries, IFormatter[] formatters)
            : base(console, factory, goQueries, formatters)
        {
        }
        public override bool IsValid(string cmd)
        {
            using (var fac = factory.Create())
            {
                var repo = fac.Create();
                var player = goQueries.GetPlayer(repo);
                var name = cmd;
                var destination = goQueries.GetExitByNameAndPlayerLocation(repo, name, player);
                if (destination == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        protected override bool ExecuteWithData(string cmd, IRepository repo, Player player)
        {
            var name = cmd;
            var destination = goQueries.GetExitByNameAndPlayerLocation(repo, name, player);
            player.Location = destination.Destination;
            console.Write("You are now in: ");
            console.WriteLine(player.Location.Name);
            return true;
        }
    }
}
