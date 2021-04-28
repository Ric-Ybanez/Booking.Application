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
    [Table("UserLogins")]
    public class User : UserEntity<int>
    {
        [MaxLength(50)]
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string Salt { get; set; }
        [Required]
        public string Role { get; set; }
        public int PersonId { get; set; }
        public int? TutorId { get; set; }
        public int? StudentId { get; set; }
        public virtual Person Person { get; set; }
        public virtual Tutor Tutor { get; set; }
        public virtual Student Student { get; set; }

        [InverseProperty("CreatedBy")]
        public ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
        [InverseProperty("CreatedBy")]
        public ICollection<Student> Students { get; set; } = new HashSet<Student>();
        [InverseProperty("CreatedBy")]
        public ICollection<Plane> Planes { get; set; } = new HashSet<Plane>();
        [InverseProperty("CreatedBy")]
        public ICollection<Tutor> Tutors { get; set; } = new HashSet<Tutor>();

        [InverseProperty("UpdatedBy")]
        public ICollection<Booking> UBookings { get; set; } = new HashSet<Booking>();
        [InverseProperty("UpdatedBy")]
        public ICollection<Student> UStudents { get; set; } = new HashSet<Student>();
        [InverseProperty("UpdatedBy")]
        public ICollection<Plane> UPlanes { get; set; } = new HashSet<Plane>();
        [InverseProperty("UpdatedBy")]
        public ICollection<Tutor> UTutors { get; set; } = new HashSet<Tutor>();
    }
}
