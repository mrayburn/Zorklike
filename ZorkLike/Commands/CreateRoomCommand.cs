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
            var name = cmd.Split(' ')[1];
            var destinationName = cmd.Split(' ')[2];
            var destination = repo.AsQueryable<GameObject>()
                .OfType<Room>().FirstOrDefault(m => m.Name == destinationName);
            var exit = new Exit() { Name = name, Destination = destination, Location = player.Location };
            repo.Add(exit);
            return true;
        }
    }
    public class CreateRoomCommand : BaseDataCommand
    {
        public CreateRoomCommand(IConsoleFacade console, IRepositoryFactoryFactory factory, IGameObjectQueries goQueries, IFormatter[] formatters)
            : base(console, factory, goQueries, formatters)
        {
            AddCommandName("@createroom");
        }
        protected override bool ExecuteWithData(string cmd, IRepository repo, Player player)
        {
            var name = cmd.Split(' ')[1];
            var room = new Room() { Name = name };
            repo.Add(room);
            return true;
        }
    }
}
