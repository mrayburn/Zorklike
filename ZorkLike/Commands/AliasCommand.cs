using System;
using System.Collections.Generic;
using System.Linq;
using ZorkLike.Data;

namespace ZorkLike.Commands
{
    public class AliasCommand : BaseDataCommand
    {
        public AliasCommand(IConsoleFacade console, IRepositoryFactoryFactory factory, IGameObjectQueries goQueries, IFormatter[] formatters)
            : base(console, factory, goQueries, formatters)
        {
            AddCommandName("@alias");
        }
        protected override bool ExecuteWithData(string cmd, IRepository repo, Player player)
        {
            var name = cmd.Split(' ')[1];
            var opType = cmd.Split(' ')[2];
            var fieldInput = cmd.Split('=')[1].Trim();
            var go = goQueries.GetGameObjectByName(repo, name);
            if (opType == "+=" && go != null)
            {
                var fieldTag = repo.AsQueryable<Tag>().FirstOrDefault(m => m.Value == fieldInput);
                if (fieldTag == null)
                {
                    fieldTag = new Tag();
                    fieldTag.Value = fieldInput;
                }
                go.Aliases.Add(fieldTag);
                console.WriteLine("Alias set!");
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
                    if (opType == "-=")
                    {
                        var fieldTag = go.Aliases.FirstOrDefault(m => m.Value == fieldInput);
                        go.Aliases.Remove(fieldTag);
                        console.WriteLine("Alias removed!");
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
