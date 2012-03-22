using System;
using System.Collections.Generic;
using System.Linq;
using ZorkLike.Data;

namespace ZorkLike.Commands
{
    public class StatusCommand : BaseDataCommand
    {
        public StatusCommand(IConsoleFacade console, IRepositoryFactoryFactory factory, IGameObjectQueries goQueries, IFormatter[] formatters)
            : base(console, factory, goQueries, formatters)
        {
            // New format for this command: @status GameObject.opType(+=, -=) fieldInput
            AddCommandName("@status");
        }
        protected override bool ExecuteWithData(string cmd, IRepository repo, Player player)
        {
            var preName = cmd.Split(new[] { ' ' }, 2);
            var name = preName[1].Split('.')[0];
            var preOpType = preName[1].Split(new[] { '.' }, 2);
            var opType = preOpType[1].Split('=')[0];
            var fieldInput = cmd.Split('=')[1].Trim();
            var go = goQueries.GetGameObjectByNameAndPlayerLocation(repo, name, player);
            if (opType == "+" && go != null)
            {
                var fieldTag = repo.AsQueryable<Tag>().FirstOrDefault(m => m.Value == fieldInput);
                if (fieldTag == null)
                {
                    fieldTag = new Tag();
                    fieldTag.Value = fieldInput;
                }
                go.Statuses.Add(fieldTag);
                console.WriteLine("Status set!");
                return true;
            }
            else
            {
                if (go == null)
                {
                    console.Write("I don't recognize: ");
                    console.WriteLine(name);
                    return false;
                }
                else
                {
                    if (opType == "-")
                    {
                        var fieldTag = go.Statuses.FirstOrDefault(m => m.Value == fieldInput);
                        go.Statuses.Remove(fieldTag);
                        console.WriteLine("Status removed!");
                        return true;
                    }
                    else
                    {
                        console.Write("I don't recognize: ");
                        console.WriteLine(opType);
                        return false;
                    }
                }
            }
        }
    }
}
