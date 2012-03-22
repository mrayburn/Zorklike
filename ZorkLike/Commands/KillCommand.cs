using System;
using System.Collections.Generic;
using System.Linq;
using ZorkLike.Data;

namespace ZorkLike.Commands
{
    public class KillCommand : BaseDataCommand
    {

        public KillCommand(IConsoleFacade console, IRepositoryFactoryFactory factory, IGameObjectQueries goQueries, IFormatter[] formatters)
            : base(console, factory, goQueries, formatters)
        {
            AddCommandName("@kill");
            AddCommandName("@delete");
        }


        protected override bool ExecuteWithData(string cmd, IRepository repo, Player player)
        {
            var name = cmd.Split(new[] {' '}, 2)[1];
            var go = goQueries.GetGameObjectByNameAndPlayerLocation(repo, name, player);
            if (go == null)
            {
                console.WriteLine("You don't see anything like that around");
                return false;
            }
            else
            {
                console.Write("You just deleted: ");
                console.WriteLine(go.Name);
                repo.Delete(go);
                return true;
            }
        }
    }
}
