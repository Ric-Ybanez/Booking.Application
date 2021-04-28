using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DAL.Models
{
    public partial class ChangeEvent
    {
        public long ChangeEventID { get; set; }
        public int UserId { get; set; }
        public DateTime DateRecorded { get; set; } = DateTime.Now;
        public virtual ICollection<ChangeLog> ChangeLogs { get; set; } = new HashSet<ChangeLog>();
    }
}
