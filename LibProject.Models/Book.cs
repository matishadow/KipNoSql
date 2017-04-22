using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibProject.Models
{
    public class Book : Entity
    {
        public string Title { get; set; }
        public Guid AuthorId { get; set; }
        public string ISBN { get; set; }

        public override string ToString()
        {
            return $"{nameof(Title)}: {Title}, {nameof(AuthorId)}: {AuthorId}, {nameof(ISBN)}: {ISBN}";
        }
    }
}
