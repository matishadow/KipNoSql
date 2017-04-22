using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibProject.Models
{
    public class Rental : Entity
    {
        public Rental(int durationInDays)
        {
            RentalDateTime = DateTime.Now;
            DurationInDays = durationInDays;

            ReturnDateTime = RentalDateTime.AddDays(DurationInDays);
        }

        public Guid RenterId { get; set; }
        public Guid BookId { get; set; }
        public DateTime RentalDateTime { get; set; }
        public DateTime ReturnDateTime { get; set; }
        public int DurationInDays { get; set; }
    }
}
