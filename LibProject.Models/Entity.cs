using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibProject.Models
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
