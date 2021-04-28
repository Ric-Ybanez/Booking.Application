using Booking.Application.Common.Models;
using Booking.Application.Common.Request;
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
    public class PlaneService : IPlaneService
    {
        private readonly ILogger<PlaneService> _logger;
        private readonly IPlaneRepository _repository;

        public PlaneService(ILogger<PlaneService> logger, IPlaneRepository repository)
        {
            this._logger = logger;
            this._repository = repository;
        }

        public async Task<int> CreatePlaneAsync(PlaneCreateRequest request)
        {
            var practice = await _repository.GetFirstAsync<DAL.Models.Plane, PlaneDto>(asNoTracking: false, filter: i=> i.Name == request.Name);
            if (practice != null)
                throw new ArgumentException("Plane already exist");

            var newPracticeId = await _repository.CreatePlaneAsync(request);
            return newPracticeId;
        }

        public async Task<PlaneDto> GetPlaneByIdAsync(int id) => await _repository.GetPlaneByIdAsync(id);

        public async Task<IEnumerable<PlaneDto>> GetPlanesAsync() => await _repository.GetPlanesAsync();

        public Task<PlaneDto> UpdatePlaneAsync(PlaneDto request, int id) => _repository.UpdatePlaneAsync(request, id);


        //Airports
        public async Task<int> CreateAirportAsync(AirportDto request)
        {
            var practice = await _repository.GetFirstAsync<DAL.Models.Airport, AirportDto>(asNoTracking: false, filter: i=> i.Name == request.Name);
            if (practice != null)
                throw new ArgumentException("Airport already exist");

            var newPracticeId = await _repository.CreateAirportAsync(request);
            return newPracticeId;
        }

        public async Task<AirportDto> GetAirportByIdAsync(int id) => await _repository.GetAirportByIdAsync(id);

        public async Task<IEnumerable<AirportDto>> GetAirportsAsync() => await _repository.GetAirportsAsync();

        public Task<AirportDto> UpdateAirportAsync(AirportDto request, int id) => _repository.UpdateAirportAsync(request, id);
    }
}
