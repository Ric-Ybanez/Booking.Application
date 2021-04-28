using Booking.Application.Repository.Implementations;
using Booking.Application.Repository.Interfaces;
using Booking.Application.Services.Implementations;
using Booking.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Application.Config
{
    public static class ServiceUtilities
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IBookingRepository, BookingRepository>();
            services.AddTransient<ITutorRepository,TutorRepository>();
            services.AddTransient<IPlaneRepository, PlaneRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();

        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IBookingService, BookingService>();
            services.AddTransient<ITutorService, TutorService>();
            services.AddTransient<IPlaneService, PlaneService>();
            services.AddTransient<IAccountService, AccountService>();
        }
    }
}
