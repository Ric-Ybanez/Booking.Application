using Booking.Application.DAL.Abstracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace Booking.Application.DAL.Models
{
    public class Booking : Entity<int>
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public bool IsConfirmed { get; set; } = false;
        public int BookingTypeId { get; set; }
        public int? TutorId { get; set; }
        public int? PlaneId { get; set; }
        public int StudentId { get; set; }
        public virtual BookingType BookingType { get; set; }
        public virtual Tutor Tutor { get; set; }
        public virtual Plane Plane { get; set; }
        public virtual Student Student { get; set; }
    }
}
