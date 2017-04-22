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
        public Language Language { get; set; }
        public string ISBN { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}
