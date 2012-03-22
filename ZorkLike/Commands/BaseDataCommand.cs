using System;
using System.Collections.Generic;
using System.Linq;
using ZorkLike.Data;

namespace ZorkLike.Commands
{
    public abstract class BaseDataCommand : BaseCommand
    {
        protected readonly IRepositoryFactoryFactory factory;
        protected readonly IGameObjectQueries goQueries;
        public BaseDataCommand(IConsoleFacade console, IRepositoryFactoryFactory factory, IGameObjectQueries goQueries, IFormatter[] formatters)
            : base(console, formatters)
        {
            this.factory = factory;
            this.goQueries = goQueries;
        }
        public override void Execute(string cmd)
        {
            using (var fac = factory.Create())
            {
                var repo = fac.Create();
                var player = goQueries.GetPlayer(repo);

                // do something 

                try
                {
                    bool saveChanges = ExecuteWithData(cmd, repo, player);
                    if (saveChanges)
                        repo.UnitOfWork.SaveChanges();
                }
                catch(Exception ex)
                {
                    //console.WriteLine(ex.Message);
                    console.WriteLine(ex.ToString());
                }
            }
        }

        protected abstract bool ExecuteWithData(string cmd, IRepository repo, Player player);
    }
}
