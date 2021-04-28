using Booking.Application.DAL.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DAL.Models
{
    public class Tutor : Entity<int>
    {
        public DateTime AvailableDateFrom { get; set; }
        public DateTime AvailableDateTo { get; set; }

        [InverseProperty("Tutor")]
        public ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
    }
}
