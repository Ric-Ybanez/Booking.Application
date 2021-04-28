using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Common.Models
{
    public class StudentDto
    {
        [MaxLength(100)]
        public string School { get; set; }
        [MaxLength(20)]
        public string IDNumber { get; set; }
    }
}
