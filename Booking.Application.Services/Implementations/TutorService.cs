using Booking.Application.Common.Models;
using Booking.Application.Common.Request;
using Booking.Application.DAL.Models;
using Booking.Application.Repository.Interfaces;
using Booking.Application.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Services.Implementations
{
    public class TutorService : ITutorService
    {
        private readonly ILogger<TutorService> _logger;
        private readonly ITutorRepository _repository;

        public TutorService(ILogger<TutorService> logger, ITutorRepository repository)
        {
            this._logger = logger;
            this._repository = repository;
        }

        public async Task<int> CreateTutorAsync(TutorCreateRequest request)
        {
            var tutor = await _repository.GetFirstAsync<User, TutorDto>(asNoTracking: false, filter: i=> i.Person.FirstName == request.FirstName && i.Person.LastName == request.LastName, includeProperties: "Person,Tutor");
            if (tutor != null)
                throw new ArgumentException("Tutor already exist");

            var newPracticeId = await _repository.CreateTutorAsync(request);
            return newPracticeId;
        }

        public async Task<TutorDto> GetTutorByIdAsync(int id) => await _repository.GetTutorByIdAsync(id);

        public async Task<IEnumerable<TutorDto>> GetTutorsAsync() => await _repository.GetTutorsAsync();

        public async Task<TutorDto> UpdateTutorAsync(TutorDto request, int id) => await _repository.UpdateTutorAsync(request, id);
    }
}
