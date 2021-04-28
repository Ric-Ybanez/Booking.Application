using Booking.Application.Common.Models;
using Booking.Application.Common.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Repository.Interfaces
{
    public interface IPlaneRepository : IBaseRepository
    {
        Task<int> CreatePlaneAsync(PlaneCreateRequest request);
        Task<PlaneDto> GetPlaneByIdAsync(int id);
        Task<IEnumerable<PlaneDto>> GetPlanesAsync();
        Task<PlaneDto> UpdatePlaneAsync(PlaneDto request, int id);

        //Airport

        Task<int> CreateAirportAsync(AirportDto request);
        Task<AirportDto> GetAirportByIdAsync(int id);
        Task<IEnumerable<AirportDto>> GetAirportsAsync();
        Task<AirportDto> UpdateAirportAsync(AirportDto request, int id);
    }
}
