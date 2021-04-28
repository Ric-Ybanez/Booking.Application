using AutoMapper;
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
    public class PlaneRepository : BaseRepository<BookingAppContext>, IPlaneRepository
    {
        public PlaneRepository(BookingAppContext context, IMapper mapper, ILogger<PlaneRepository> log) : base(context, mapper, log) { }

        public async Task<int> CreatePlaneAsync(PlaneCreateRequest request)
        {
            var practice = _mapper.Map<PlaneCreateRequest, Plane>(request);

            Insert(practice);
            await SaveAsync();
            return practice.Id;
        }

        public async Task<PlaneDto> GetPlaneByIdAsync(int id)
        {
            var plane = await GetOneAsync<Plane, PlaneDto>(asNoTracking: true, filter: i => i.Id == id);
            return plane;
        }

        public async Task<IEnumerable<PlaneDto>> GetPlanesAsync()
        {
            var planes = await GetAsync<Plane, PlaneDto>(asNoTracking: true);
            return planes;
        }

        public async Task<PlaneDto> UpdatePlaneAsync(PlaneDto request, int id)
        {
            var plane = await _context.Planes.FindAsync(id);
            _mapper.Map<PlaneDto, Plane>(request, plane);
            await SaveAsync();
            return _mapper.Map<Plane, PlaneDto>(plane);
        }

        //airport

        public async Task<int> CreateAirportAsync(AirportDto request)
        {
            var practice = _mapper.Map<AirportDto, Airport>(request);

            Insert(practice);
            await SaveAsync();
            return practice.Id;
        }

        public async Task<AirportDto> GetAirportByIdAsync(int id)
        {
            var plane = await GetOneAsync<Airport, AirportDto>(asNoTracking: true, filter: i => i.Id == id);
            return plane;
        }

        public async Task<IEnumerable<AirportDto>> GetAirportsAsync()
        {
            var planes = await GetAsync<Airport, AirportDto>(asNoTracking: true);
            return planes;
        }

        public async Task<AirportDto> UpdateAirportAsync(AirportDto request, int id)
        {
            var plane = await _context.Airports.FindAsync(id);
            _mapper.Map<AirportDto, Airport>(request, plane);
            await SaveAsync();
            return _mapper.Map<Airport, AirportDto>(plane);
        }
    }
}
