using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibProject.Models
{
    public class Renter : Person
    {
        public string Address { get; set; }
        public byte Age { get; set; }
        public string TelephoneNumber { get; set; }
    }
}
