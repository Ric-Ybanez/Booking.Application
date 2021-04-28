using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DAL.Interfaces
{
    public interface IEntity
    {
        DateTime Created { get; set; }
        int CreatedById { get; set; }
        DateTime Updated { get; set; }
        int UpdatedById { get; set; }
        bool IsActive { get; set; }
    }
    public interface IEntity<T> : IEntity
    {
        T Id { get; set; }
    }
}
