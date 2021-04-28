using AutoMapper;
using Booking.Application.Common.Models;
using Booking.Application.Common.Request;
using Booking.Application.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Repository.Mapping
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            BookingMapping();

        }

        private void BookingMapping()
        {
            CreateMap<StudentCreateRequest, User>().
                ForMember(dst => dst.Username, m => m.MapFrom(src => src.Username)).
                ForMember(dst => dst.Password, m => m.MapFrom(src => src.Password)).
                ForAllOtherMembers(dst => dst.Ignore());

            CreateMap<StudentCreateRequest, Student>().
                ForMember(dst => dst.IDNumber, m => m.MapFrom(src => src.IDNumber)).
                ForMember(dst => dst.School, m => m.MapFrom(src => src.School))
                .ForAllOtherMembers(dst => dst.Ignore());

            CreateMap<StudentCreateRequest, Person>().
               ForMember(dst => dst.FirstName, m => m.MapFrom(src => src.FirstName)).
               ForMember(dst => dst.MiddleName, m => m.MapFrom(src => src.MiddleName)).
               ForMember(dst => dst.LastName, m => m.MapFrom(src => src.LastName)).
               ForMember(dst => dst.Gender, m => m.MapFrom(src => src.Gender)).
               ForMember(dst => dst.DateOfBirth, m => m.MapFrom(src => src.DateOfBirth)).
               ForMember(dst => dst.ContactNo, m => m.MapFrom(src => src.ContactNo)).
               ForMember(dst => dst.Email, m => m.MapFrom(src => src.Email)).
               ForAllOtherMembers(dst => dst.Ignore());

            CreateMap<PlaneCreateRequest, Plane>().
              ForMember(dst => dst.AirportId, m => m.MapFrom(src => src.AirportId)).
              ForMember(dst => dst.Description, m => m.MapFrom(src => src.Description)).
              ForMember(dst => dst.Name, m => m.MapFrom(src => src.Name)).
              ForMember(dst => dst.IsAvailable, m => m.MapFrom(src => src.IsAvailable)).
              ForAllOtherMembers(dst => dst.Ignore());

            CreateMap<Plane, PlaneDto>().
                ForMember(dst => dst.Id, m => m.MapFrom(src => src.Id)).
              ForMember(dst => dst.AirportId, m => m.MapFrom(src => src.AirportId)).
              ForMember(dst => dst.Description, m => m.MapFrom(src => src.Description)).
              ForMember(dst => dst.Name, m => m.MapFrom(src => src.Name)).
              ForMember(dst => dst.IsAvailable, m => m.MapFrom(src => src.IsAvailable)).
              ForMember(dst => dst.Airport, m => m.MapFrom(src => src.Airport)).
              ForAllOtherMembers(dst => dst.Ignore());

            CreateMap<PlaneDto, Plane>().
              ForMember(dst => dst.AirportId, m => m.MapFrom(src => src.AirportId)).
              ForMember(dst => dst.Description, m => m.MapFrom(src => src.Description)).
              ForMember(dst => dst.Name, m => m.MapFrom(src => src.Name)).
              ForMember(dst => dst.IsAvailable, m => m.MapFrom(src => src.IsAvailable)).
              ForAllOtherMembers(dst => dst.Ignore());

            CreateMap<Airport, AirportDto>().
            ReverseMap();

            CreateMap<TutorCreateRequest, User>().
              ForMember(dst => dst.Username, m => m.MapFrom(src => src.Username)).
              ForMember(dst => dst.Password, m => m.MapFrom(src => src.Password)).
              ForMember(dst => dst.Role, m => m.MapFrom(src => "Tutor")).
              ForMember(dst => dst.Salt, m => m.MapFrom(src => src.Salt)).
              ForAllOtherMembers(dst => dst.Ignore());

            CreateMap<TutorCreateRequest, Person>().
             ForMember(dst => dst.FirstName, m => m.MapFrom(src => src.FirstName)).
             ForMember(dst => dst.LastName, m => m.MapFrom(src => src.LastName)).
             ForMember(dst => dst.ContactNo, m => m.MapFrom(src => src.ContactNo)).
             ForMember(dst => dst.DateOfBirth, m => m.MapFrom(src => src.DateOfBirth)).
             ForMember(dst => dst.Email, m => m.MapFrom(src => src.Email)).
             ForMember(dst => dst.Gender, m => m.MapFrom(src => src.Gender)).
             ForMember(dst => dst.MiddleName, m => m.MapFrom(src => src.MiddleName)).
             ForAllOtherMembers(dst => dst.Ignore());

            CreateMap<TutorCreateRequest, Tutor>().
             ForMember(dst => dst.AvailableDateFrom, m => m.MapFrom(src => src.AvailableDateTo)).
             ForMember(dst => dst.AvailableDateTo, m => m.MapFrom(src => src.AvailableDateTo)).
             ForAllOtherMembers(dst => dst.Ignore());

            CreateMap<User, TutorDto>().
            ForMember(dst => dst.Id, m => m.MapFrom(src => src.Id)).
            ForMember(dst => dst.TutorId, m => m.MapFrom(src => src.Tutor.Id)).
            ForMember(dst => dst.PersonId, m => m.MapFrom(src => src.Person.Id)).
            ForMember(dst => dst.AvailableDateTo, m => m.MapFrom(src => src.Tutor.AvailableDateTo)).
            ForMember(dst => dst.AvailableDateFrom, m => m.MapFrom(src => src.Tutor.AvailableDateFrom)).
            ForMember(dst => dst.FirstName, m => m.MapFrom(src => src.Person.FirstName)).
            ForMember(dst => dst.LastName, m => m.MapFrom(src => src.Person.LastName)).
            ForMember(dst => dst.MiddleName, m => m.MapFrom(src => src.Person.MiddleName)).
            ForMember(dst => dst.DateOfBirth, m => m.MapFrom(src => src.Person.DateOfBirth)).
            ForMember(dst => dst.Email, m => m.MapFrom(src => src.Person.Email)).
            ForMember(dst => dst.Gender, m => m.MapFrom(src => src.Person.Gender)).
            ForMember(dst => dst.ContactNo, m => m.MapFrom(src => src.Person.ContactNo)).
            ForAllOtherMembers(dst => dst.Ignore());

            CreateMap<TutorDto, User>().
            ForMember(dst => dst.Id, m => m.MapFrom(src => src.Id)).
            ForMember(dst => dst.Tutor, m => m.MapFrom(src => src)).
            ForMember(dst => dst.Person, m => m.MapFrom(src => src)).
            ForAllOtherMembers(dst => dst.Ignore());

            CreateMap<TutorDto, Tutor>().
            ForMember(dst => dst.AvailableDateFrom, m => m.MapFrom(src => src.AvailableDateTo)).
            ForMember(dst => dst.AvailableDateFrom, m => m.MapFrom(src => src.AvailableDateFrom)).
            ForAllOtherMembers(dst => dst.Ignore());

            CreateMap<TutorDto, Person>().
            ForMember(dst => dst.FirstName, m => m.MapFrom(src => src.FirstName)).
            ForMember(dst => dst.LastName, m => m.MapFrom(src => src.LastName)).
            ForMember(dst => dst.MiddleName, m => m.MapFrom(src => src.MiddleName)).
            ForMember(dst => dst.DateOfBirth, m => m.MapFrom(src => src.DateOfBirth)).
            ForMember(dst => dst.Email, m => m.MapFrom(src => src.Email)).
            ForMember(dst => dst.Gender, m => m.MapFrom(src => src.Gender)).
            ForMember(dst => dst.ContactNo, m => m.MapFrom(src => src.ContactNo)).
            ForAllOtherMembers(dst => dst.Ignore());
        }
    }
}
