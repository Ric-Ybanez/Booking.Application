using Booking.Application.DAL.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DAL.Models
{
    public class Plane : Entity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int AirportId { get; set; }
        public bool IsAvailable { get; set; } = true;
        public virtual Airport Airport { get; set; }

        [InverseProperty("Plane")]
        public ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
    }
}
