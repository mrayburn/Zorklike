using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace ZorkLike.Data
{
    public class Exit : GameObject
    {
        public virtual Room Destination { get; set; }
    }
}
