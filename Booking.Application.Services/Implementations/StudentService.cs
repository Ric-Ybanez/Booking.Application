using Booking.Application.Common.Models;
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
    public class StudentService : IStudentService
    {
        private readonly ILogger<StudentService> _logger;
        private readonly IStudentRepository _repository;

        public StudentService(ILogger<StudentService> logger, IStudentRepository repository)
        {
            this._logger = logger;
            this._repository = repository;
        }

        public async Task<int> CreateStudentAsync(StudentDto request)
        {
            var practice = await _repository.GetFirstAsync<Student, StudentDto>(asNoTracking: false);
            if (practice != null)
                throw new ArgumentException("Booking already exist");

            var newPracticeId = await _repository.CreateStudentAsync(request);
            return newPracticeId;
        }

        public async Task<IEnumerable<StudentDto>> GetStudentAsync() => await _repository.GetStudentsAsync();

        public async Task<StudentDto> GetStudentByIdAsync(int id) => await _repository.GetStudentByIdAsync(id);

        public async Task<StudentDto> UpdateStudentAsync(StudentDto request, int id) => await _repository.UpdateStudentAsync(request, id);
    }
}
