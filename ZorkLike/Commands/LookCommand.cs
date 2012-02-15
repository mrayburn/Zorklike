using System;
using System.Collections.Generic;
using System.Linq;
using ZorkLike.Data;

namespace ZorkLike.Commands
{
    public class LookCommand : BaseDataCommand
    {
        public LookCommand(IConsoleFacade console, IRepositoryFactoryFactory factory, IGameObjectQueries goQueries, IFormatter[] formatters)
            : base (console, factory, goQueries, formatters)
        {
            AddCommandName("look");
            AddCommandName("l");
        }
        
        protected override bool ExecuteWithData(string cmd, IRepository repo, Player player)
        {
            string[] cmdArray = cmd.Split(new [] { ' ' }, 2);
            string name = "";
            if (cmdArray.GetUpperBound(0) >= 1)
                name = cmdArray[1].Trim();
            if (name == "here" || name == "")
            {
                formatters.OfType<LookFormatter>().First().Format(player.Location);
                formatters.OfType<InventoryFormatter>().First().Format(player.Location);
            }
            else
            {
                var go = goQueries.GetGameObjectByNameAndPlayerLocation(repo, name, player);
                if (go == null)
                {
                    formatters.OfType<NullFormatter>().First().Format(go);
                    //console.WriteLine("You can't find anything like that around.");
                }
                else
                {
                    formatters.OfType<LookFormatter>().First().Format(go);
                    formatters.OfType<InventoryFormatter>().First().Format(go);
                }
            }
            return false;
        }
    }
}
