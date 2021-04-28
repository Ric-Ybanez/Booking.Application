using Booking.Application.Common.Models;
using Booking.Application.Common.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Services.Interfaces
{
    public interface IPlaneService
    {

        // Plane
        Task<int> CreatePlaneAsync(PlaneCreateRequest request);
        Task<PlaneDto> UpdatePlaneAsync(PlaneDto request, int id);
        Task<IEnumerable<PlaneDto>> GetPlanesAsync();
        Task<PlaneDto> GetPlaneByIdAsync(int id);

        //Airport
        Task<int> CreateAirportAsync(AirportDto request);
        Task<AirportDto> UpdateAirportAsync(AirportDto request, int id);
        Task<IEnumerable<AirportDto>> GetAirportsAsync();
        Task<AirportDto> GetAirportByIdAsync(int id);
    }
}
