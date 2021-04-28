using AutoMapper;
using Booking.Application.Common.Models;
using Booking.Application.DAL;
using Booking.Application.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Booking.Application.Repository.Implementations
{
    public class BookingRepository : BaseRepository<BookingAppContext>, IBookingRepository
    {
        public BookingRepository(BookingAppContext context, IMapper mapper, ILogger<BookingRepository> log) : base(context, mapper, log) { }

        public async Task<int> CreateBookingAsync(BookingDto request)
        {
            var practice = _mapper.Map<BookingDto, DAL.Models.Booking>(request);

            Insert(practice);
            await SaveAsync();
            return practice.Id;
        }
    }
}
