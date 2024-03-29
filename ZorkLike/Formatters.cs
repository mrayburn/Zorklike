using System;
using System.Collections.Generic;
using System.Linq;
using ZorkLike.Commands;
using ZorkLike.Data;

namespace ZorkLike
{

    public class NullFormatter : IFormatter
    {
        private IConsoleFacade console;

        public NullFormatter(IConsoleFacade console)
        {
            this.console = console;
        }

        public void Format(Data.GameObject go)
        {
            console.WriteLine("You can't find anything like that around.");
        }
    }
    public class LookFormatter : IFormatter
    {
        private IConsoleFacade console;

        public LookFormatter(IConsoleFacade console)
        {
            this.console = console;
        }

        public void Format(Data.GameObject go)
        {

            console.WriteLine(go.Name);
            console.WriteLine(go.Description ?? "You see nothing special about it.");
            console.WriteLine("");
        }
    }
    public class InventoryFormatter : IFormatter
    {
        private IConsoleFacade console;

        public InventoryFormatter(IConsoleFacade console)
        {
            this.console = console;
        }

        public void Format(Data.GameObject go)
        {
            var count = 0;
            console.WriteLine("Inventory:");
            foreach (var item in go.Inventory)
            {
                if (typeof(Exit).IsInstanceOfType(item) == false && 
                    typeof(Player).IsInstanceOfType(item) == false &&
                    item.Statuses.FirstOrDefault(m => m.Value == "hidden") == null)
                {
                    console.Write(string.Format("{0,-20}", item.Name));
                    count++;
                    if (count % 3 == 0)
                    {
                        console.WriteLine("");
                    }
                }
            }
            console.WriteLine("");
            count = 0;
            console.WriteLine("Exits:");
            foreach (var item in go.Inventory)
            {
                if (typeof(Exit).IsInstanceOfType(item) == true &&
                    item.Statuses.FirstOrDefault(m => m.Value == "hidden") == null)
                {
                    console.Write(string.Format("{0,-20}", item.Name));
                    count++;
                    if (count % 3 == 0)
                    {
                        console.WriteLine("");
                    }
                }
            }
            console.WriteLine("");
        }
    }
}
