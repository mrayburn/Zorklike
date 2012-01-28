using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace ZorkLike.Data
{
    public class GameObject
    {
        public GameObject()
        {
            Aliases = new List<Tag>();
            Statuses = new List<Tag>();
            Inventory = new List<GameObject>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual List<Tag> Aliases { get; set; }
        public string Description { get; set; }
        public virtual List<Tag> Statuses { get; set; }
        public virtual GameObject Location { get; set; }
        public virtual List<GameObject> Inventory { get; set; }
    }
}