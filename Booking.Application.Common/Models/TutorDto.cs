using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Common.Models
{
    public class TutorDto
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int TutorId { get; set; }
        public DateTime AvailableDateFrom { get; set; }
        public DateTime AvailableDateTo { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
    }
}
