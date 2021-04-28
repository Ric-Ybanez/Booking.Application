using Booking.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Services.Interfaces
{
    public interface IBookingService
    {
        Task<int> CreateBookingAsync(BookingDto request);
    }
}
