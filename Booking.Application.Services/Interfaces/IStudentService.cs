using Booking.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Services.Interfaces
{
    public interface IStudentService
    {
        Task<int> CreateStudentAsync(StudentDto request);
        Task<StudentDto> UpdateStudentAsync(StudentDto request, int id);
        Task<IEnumerable<StudentDto>> GetStudentAsync();
        Task<StudentDto> GetStudentByIdAsync(int id);
    }
}
