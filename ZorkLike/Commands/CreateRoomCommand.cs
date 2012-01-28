using System;
using System.Collections.Generic;
using System.Linq;
using ZorkLike.Data;

namespace ZorkLike.Commands
{
    public class CreateRoomCommand : ICommand
    {
        private readonly IRepositoryFactoryFactory factory;
        private readonly IConsoleFacade console;
        public CreateRoomCommand(IConsoleFacade console, IRepositoryFactoryFactory factory)
        {
            this.console = console;
            this.factory = factory;
        }
        public bool IsValid(string cmd)
        {
            return cmd.Split(' ')[0].ToLower() == "@createroom";
        }
        public void Execute(string cmd)
        {
            using (var fac = factory.Create())
            {
                var repo = fac.Create();
                var name = cmd.Split(' ')[1];
                var room = new Room();
                room.Name = name;
                repo.Add(room);
                repo.UnitOfWork.SaveChanges();
            }
        }
    }
}
