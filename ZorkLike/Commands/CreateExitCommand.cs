using System;
using System.Collections.Generic;
using System.Linq;
using ZorkLike.Data;

namespace ZorkLike.Commands
{
    public class CreateExitCommand : BaseDataCommand
    {
        public CreateExitCommand(IConsoleFacade console, IRepositoryFactoryFactory factory, IGameObjectQueries goQueries, IFormatter[] formatters)
            : base(console, factory, goQueries, formatters)
        {
            AddCommandName("@createexit");
        }

        protected override bool ExecuteWithData(string cmd, IRepository repo, Player player)
        {
            var preName = cmd.Split(new[] {' '}, 2);
            var name = preName[1].Split('=')[0].Trim();
            var destinationName = cmd.Split('=')[1].Trim();
            var destination = repo.AsQueryable<GameObject>()
                .OfType<Room>().FirstOrDefault(m => m.Name == destinationName);
            var exit = new Exit() { Name = name.Trim(), Destination = destination, Location = player.Location };
            repo.Add(exit);
            console.WriteLine("Exit created");
            return true;
        }
    }
}
