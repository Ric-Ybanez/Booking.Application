using Booking.Application.Common.Models;
using Booking.Application.Common.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Services.Interfaces
{
    public interface ITutorService
    {
        Task<int> CreateTutorAsync(TutorCreateRequest request);
        Task<TutorDto> UpdateTutorAsync(TutorDto request, int id);
        Task<IEnumerable<TutorDto>> GetTutorsAsync();
        Task<TutorDto> GetTutorByIdAsync(int id);
    }
}
