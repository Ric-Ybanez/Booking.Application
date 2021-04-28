using AutoMapper;
using Booking.Application.Common.Models;
using Booking.Application.DAL;
using Booking.Application.DAL.Models;
using Booking.Application.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Repository.Implementations
{
    public class StudentRepository : BaseRepository<BookingAppContext>, IStudentRepository
    {
        public StudentRepository(BookingAppContext context, IMapper mapper, ILogger<StudentRepository> log) : base(context, mapper, log) { }

        public async Task<int> CreateStudentAsync(StudentDto request)
        {
            var practice = _mapper.Map<StudentDto, DAL.Models.Student>(request);

            Insert(practice);
            await SaveAsync();
            return practice.Id;
        }

        public async Task<StudentDto> GetStudentByIdAsync(int id)
        {
            var student = await GetOneAsync<Student, StudentDto>(asNoTracking: true, filter: i => i.Id == id);
            return student;
        }

        public async Task<IEnumerable<StudentDto>> GetStudentsAsync()
        {
            var students = await GetAsync<Student, StudentDto>(asNoTracking: true);
            return students;
        }

        public async Task<StudentDto> UpdateStudentAsync(StudentDto request, int id)
        {
            var student = await _context.Students.FindAsync(id);
            _mapper.Map<StudentDto, Student>(request, student);
            await SaveAsync();
            return _mapper.Map<Student, StudentDto>(student);
        }
    }
}
