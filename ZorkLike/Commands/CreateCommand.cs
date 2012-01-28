using System;
using System.Collections.Generic;
using System.Linq;
using ZorkLike.Data;

namespace ZorkLike.Commands
{
    public class CreateCommand : ICommand
    {
        private readonly IRepositoryFactoryFactory factory;
        private readonly IConsoleFacade console;
        private readonly IGameObjectQueries goQueries;
        public CreateCommand(IConsoleFacade console, IRepositoryFactoryFactory factory, IGameObjectQueries goQueries)
        {
            this.console = console;
            this.factory = factory;
            this.goQueries = goQueries;
        }
        public bool IsValid(string cmd)
        {
            return cmd.Split(' ')[0].ToLower() == "@create";
        }
        public void Execute(string cmd)
        {
            using (var fac = factory.Create())
            {
                 
                var repo = fac.Create();
                var name = cmd.Split(' ')[1];
                var go = new GameObject();
                go.Name = name;
                var player = goQueries.GetPlayer(repo);
                go.Location = player.Location;
                repo.Add(go);
                repo.UnitOfWork.SaveChanges();
            }
        }
    }
}
