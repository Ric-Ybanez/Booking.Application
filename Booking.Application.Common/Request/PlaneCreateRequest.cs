using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Common.Request
{
    public class PlaneCreateRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int AirportId { get; set; }
        public bool IsAvailable { get; set; } = true;
    }
}
