using System;
using System.Collections.Generic;
using System.Linq;
using ZorkLike.Data;

namespace ZorkLike.Commands
{
    public class PickUpCommand : BaseDataCommand
    {
        public PickUpCommand(IConsoleFacade console, IRepositoryFactoryFactory factory, IGameObjectQueries goQueries, IFormatter[] formatters)
            : base(console, factory, goQueries, formatters)
        {
            AddCommandName("pickup");
            AddCommandName("get");
            AddCommandName("grab");
        }
        protected override bool ExecuteWithData(string cmd, IRepository repo, Player player)
        {
            var goName = cmd.Split(new[] { ' ' }, 2)[1];
            var go = goQueries.GetGameObjectByNameAndLocation(repo, goName, player.Location);
            if (go == null)
            {
                console.WriteLine("You can't find anything like that around.");
                return false;
            }
            else
            {
                var holding = player.Inventory.FirstOrDefault(m => m.Name == goName);
                if (holding != null)
                {
                    console.WriteLine("You are already carrying that.");
                    return false;
                }
                else
                {
                    player.Inventory.Add(go);
                    console.Write("You are now holding: ");
                    console.WriteLine(goName);
                    return true;
                }
            }
        }
    }
}
