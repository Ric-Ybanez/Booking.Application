using Booking.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Repository.Interfaces
{
    public interface IAccountRepository : IBaseRepository
    {
        Task<int> LoginAsync(AccountDto request);
    }
}
