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
    public class Student : Entity<int>
    {
        [MaxLength(100)]
        public string School { get; set; }
        [MaxLength(20)]
        public string IDNumber { get; set; }

        [InverseProperty("Student")]
        public ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
    }
}