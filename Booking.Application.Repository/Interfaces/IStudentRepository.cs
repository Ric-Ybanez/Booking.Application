using Booking.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Repository.Interfaces
{
    public interface IStudentRepository : IBaseRepository
    {
        Task<int> CreateStudentAsync(StudentDto request);
        Task<StudentDto> GetStudentByIdAsync(int id);
        Task<IEnumerable<StudentDto>> GetStudentsAsync();
        Task<StudentDto> UpdateStudentAsync(StudentDto request, int id);
    }
}
