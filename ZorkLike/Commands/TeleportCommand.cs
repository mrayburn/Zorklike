using System;
using System.Collections.Generic;
using System.Linq;
using ZorkLike.Data;

namespace ZorkLike.Commands
{
    public class TeleportCommand : BaseDataCommand
    {
        public TeleportCommand(IConsoleFacade console, IRepositoryFactoryFactory factory, IGameObjectQueries goQueries, IFormatter[] formatters)
            : base(console, factory, goQueries, formatters)
        {
            AddCommandName("@teleport");
        }
        protected override bool ExecuteWithData(string cmd, IRepository repo, Player player)
        {
            var locationName = cmd.Split(new[] { ' ' }, 2)[1];
            var location = repo.AsQueryable<GameObject>()
                .OfType<Room>().FirstOrDefault(m => m.Name == locationName);
            if (player.Location == location)
            {
                console.WriteLine("Really!? You are already there!");
                return false;
            }
            else
            {
                player.Location = location;
                console.Write("You have teleported to: ");
                console.WriteLine(player.Location.Name);
                return true;
            }
        }
    }
}
