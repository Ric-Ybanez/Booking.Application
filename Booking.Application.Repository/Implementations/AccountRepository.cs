using AutoMapper;
using Booking.Application.Common.Models;
using Booking.Application.DAL;
using Booking.Application.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Repository.Implementations
{
    public class AccountRepository : BaseRepository<BookingAppContext>, IAccountRepository
    {
        public AccountRepository(BookingAppContext context, IMapper mapper, ILogger<AccountRepository> log) : base(context, mapper, log) { }

        public Task<int> LoginAsync(AccountDto request) 
        {
            return null;
        }
    }
}
