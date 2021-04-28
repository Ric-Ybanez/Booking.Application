using Booking.Application.DAL.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DAL.Models
{
    public class Airport : Entity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsAvailable { get; set; } = true;

        [InverseProperty("Airport")]
        public ICollection<Plane> Planes { get; set; } = new HashSet<Plane>();
    }
}
