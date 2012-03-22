using System;
using System.Collections.Generic;
using System.Linq;

namespace ZorkLike.Data
{
    public class GameObjectQueries : IGameObjectQueries
    {
        public Room GetMapRoom(IRepository repo)
        {
            var mapRoom = repo.AsQueryable<GameObject>().OfType<Room>().FirstOrDefault(m => m.Name == "MapRoom");
            if (mapRoom == null)
            {
                mapRoom = new Room();
                mapRoom.Name = "The Map Room";
                repo.Add(mapRoom);
                repo.UnitOfWork.SaveChanges();
            }
            return mapRoom;
        }
        public Player GetPlayer(IRepository repo)
        {
            var player = repo.AsQueryable<GameObject>().OfType<Player>().FirstOrDefault();
            if (player == null)
            {
                player = new Player();
                player.Location = GetMapRoom(repo);
                player.Aliases.Add(new Tag { Value = "Me" });
                player.Aliases.Add(new Tag { Value = "Player" });
                repo.Add(player);
                repo.UnitOfWork.SaveChanges();
            }
            return player;
        }
        public GameObject GetGameObjectByName(IRepository repo, string name)
        {
            var go = repo.AsQueryable<GameObject>().FirstOrDefault(m => m.Name == name || m.Aliases.Any(n => n.Value == name));
            return go;
        }
        public GameObject GetGameObjectByNameAndLocation(IRepository repo, string name, GameObject location)
        {
            var go = repo.AsQueryable<GameObject>().FirstOrDefault(m => (m.Name == name || m.Aliases.Any(n => n.Value == name)) && m.Location.Id == location.Id);
            return go;
        }
        public GameObject GetGameObjectByNameAndPlayerLocation(IRepository repo, string name, Player player)
        {
            var go = repo.AsQueryable<GameObject>().FirstOrDefault(m => (m.Name == name || m.Aliases.Any(x => x.Value == name)) && (m.Location.Id == player.Id || m.Location.Id == player.Location.Id));
            return go;
        }
        public Exit GetExitByNameAndPlayerLocation(IRepository repo, string name, Player player)
        {
            var exit = repo.AsQueryable<Exit>().FirstOrDefault(m => (m.Name == name || m.Aliases.Any(x => x.Value == name)) && (m.Location.Id == player.Id || m.Location.Id == player.Location.Id));
            return exit;
        }
    }
}
