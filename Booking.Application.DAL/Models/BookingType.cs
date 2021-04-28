using Booking.Application.DAL.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DAL.Models
{
    public class BookingType : Entity<int>
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(300)]
        public string Description { get; set; }
        [InverseProperty("BookingType")]
        public ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
    }
}
