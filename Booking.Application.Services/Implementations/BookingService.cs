using Booking.Application.Common.Models;
using Booking.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Application.DAL;
using Microsoft.Extensions.Logging;
using Booking.Application.Repository.Interfaces;

namespace Booking.Application.Services.Implementations
{
    public class BookingService : IBookingService
    {
        private readonly ILogger<BookingService> _logger;
        private readonly IBookingRepository _repository;

        public BookingService(ILogger<BookingService> logger, IBookingRepository repository)
        {
            this._logger = logger;
            this._repository = repository;
        }

        public async Task<int> CreateBookingAsync(BookingDto request)
        {
            var practice = await _repository.GetFirstAsync<DAL.Models.Booking, BookingDto>(asNoTracking: false);
            if (practice != null)
                throw new ArgumentException("Booking already exist");

            var newPracticeId = await _repository.CreateBookingAsync(request);
            return newPracticeId;
        }
    }
}
