using System;
using System.Collections.Generic;
using System.Linq;
using ZorkLike.Data;

namespace ZorkLike.Commands
{
    public class CreateRoomCommand : BaseDataCommand
    {
        public CreateRoomCommand(IConsoleFacade console, IRepositoryFactoryFactory factory, IGameObjectQueries goQueries, IFormatter[] formatters)
            : base(console, factory, goQueries, formatters)
        {
            AddCommandName("@createroom");
        }
        protected override bool ExecuteWithData(string cmd, IRepository repo, Player player)
        {
            var name = cmd.Split(new[] { ' ' }, 2);
            var room = new Room() { Name = name[1].Trim() };
            repo.Add(room);
            console.WriteLine("Room created");
            return true;
        }
    }
}
