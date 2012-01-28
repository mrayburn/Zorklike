using System;
using System.Collections.Generic;
using System.Linq;
using ZorkLike.Data;

namespace ZorkLike.Commands
{
    public class TeleportCommand : ICommand
    {
        private readonly IRepositoryFactoryFactory factory;
        private readonly IConsoleFacade console;
        private readonly IGameObjectQueries goQueries;
        public TeleportCommand(IConsoleFacade console, IRepositoryFactoryFactory factory, IGameObjectQueries goQueries)
        {
            this.console = console;
            this.factory = factory;
            this.goQueries = goQueries;
        }
        public bool IsValid(string cmd)
        {
            return cmd.Split(' ')[0].ToLower() == "@teleport";
        }

        public void Execute(string cmd)
            {
                using (var fac = factory.Create())
                {
                    var repo = fac.Create();
                    var locationName = cmd.Split(' ')[1];
                    var location = repo.AsQueryable<GameObject>()
                        .OfType<Room>().FirstOrDefault(m => m.Name == locationName);
                    var player = goQueries.GetPlayer(repo);
                    if (player.Location == location)
                    {
                        console.WriteLine("Really!? You are already there!");
                    }
                    else player.Location = location;
                    repo.UnitOfWork.SaveChanges();
                }
            }
    }
}
