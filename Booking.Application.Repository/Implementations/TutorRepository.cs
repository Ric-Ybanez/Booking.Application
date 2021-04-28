using AutoMapper;
using AutoMapper.QueryableExtensions;
using Booking.Application.Common.Models;
using Booking.Application.Common.Request;
using Booking.Application.DAL;
using Booking.Application.DAL.Models;
using Booking.Application.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Repository.Implementations
{
    public class TutorRepository : BaseRepository<BookingAppContext>, ITutorRepository
    {
        public TutorRepository(BookingAppContext context, IMapper mapper, ILogger<TutorRepository> log) : base(context, mapper, log) { }

        public async Task<int> CreateTutorAsync(TutorCreateRequest request)
        {
            var user = _mapper.Map<TutorCreateRequest, User>(request);
            var tutor = _mapper.Map<TutorCreateRequest, Tutor>(request);
            var person = _mapper.Map<TutorCreateRequest, Person>(request);

            user.Tutor = tutor;
            user.Person = person;

            Insert(user);
            await SaveAsync();
            return user.Id;
        }

        public async Task<TutorDto> GetTutorByIdAsync(int id)
        {
            var tutor = await GetOneAsync<User, TutorDto>(asNoTracking: true, filter: i => i.Id == id && i.Role == "Tutor");
            return tutor;
        }

        public async Task<IEnumerable<TutorDto>> GetTutorsAsync()
        {
            var tutors = await GetAsync<User, TutorDto>(asNoTracking: true, includeProperties: "Tutor,Person", filter: i=> i.Role == "Tutor");
            return tutors;
        }

        public async Task<TutorDto> UpdateTutorAsync(TutorDto request, int id)
        {
            var tutor = await _context.Users
                .Include(x=> x.Person)
                .Include(x => x.Tutor).FirstOrDefaultAsync(x=> x.Id == id);

            _mapper.Map<TutorDto, User>(request, tutor);
            await SaveAsync();
            return request;
        }
    }
}
