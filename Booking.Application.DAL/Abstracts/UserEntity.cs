using Booking.Application.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DAL.Abstracts
{
    public abstract class UserEntity<T> : IEntity<T>
    {
        public T Id { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        [Required]
        [MaxLength(64)]
        public int CreatedById { get; set; }
        public DateTime Updated { get; set; } = DateTime.Now;
        [Required]
        [MaxLength(64)]
        public int UpdatedById { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
