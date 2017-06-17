using System;

namespace Kip.Models.Base
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
