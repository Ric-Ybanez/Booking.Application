using Booking.Application.Common.Models;
using Booking.Application.Common.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Repository.Interfaces
{
    public interface ITutorRepository : IBaseRepository
    {
        Task<int> CreateTutorAsync(TutorCreateRequest request);
        Task<TutorDto> GetTutorByIdAsync(int id);
        Task<IEnumerable<TutorDto>> GetTutorsAsync();
        Task<TutorDto> UpdateTutorAsync(TutorDto request, int id);
    }
}
