using System;
using System.Collections.Generic;
using System.Linq;
using ZorkLike.Data;

namespace ZorkLike.Commands
{
    public class SetCommand : BaseDataCommand
    {
        public SetCommand(IConsoleFacade console, IRepositoryFactoryFactory factory, IGameObjectQueries goQueries, IFormatter[] formatters)
            : base(console, factory, goQueries, formatters)
        {
            AddCommandName("@set");
        }
        protected override bool ExecuteWithData(string cmd, IRepository repo, Player player)
        {
            var preName = cmd.Split(' ')[1];
            var name = preName.Split('.')[0];
            var field = preName.Split('.')[1];
            var fieldInput = cmd.Split('=')[1];
            var go = goQueries.GetGameObjectByName(repo, name);

            if (field == "description")
            {
                go.Description = fieldInput;
                console.WriteLine("Description set!");
                return true;
            }
            else
            {
                if (field == "name")
                {
                    go.Name = fieldInput;
                    console.WriteLine("Name set!");
                    return true;
                }
                else
                {
                    console.Write("I don't recognize: ");
                    console.WriteLine(field);
                    return false;
                }
            }
        }
    }
}
