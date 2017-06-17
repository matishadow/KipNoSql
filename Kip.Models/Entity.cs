using System;

namespace Kip.Models
{
    public class Entity
    {
        public Entity()
        {
            id = Guid.NewGuid();
        }

        public Guid id { get; set; }
    }
}
