using System;
using System.Collections.Generic;
using System.Linq;

namespace ZorkLike.Data
{
    public interface IGameObjectQueries
    {
        GameObject GetGameObjectByNameAndLocation(IRepository repo, string name, GameObject location);
        GameObject GetGameObjectByName(IRepository repo, string name);
        GameObject GetGameObjectByNameAndPlayerLocation(IRepository repo, string name, Player player);
        Exit GetExitByNameAndPlayerLocation(IRepository repo, string name, Player player);
        Player GetPlayer(IRepository repo);
    }
}
