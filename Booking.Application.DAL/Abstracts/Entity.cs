using Booking.Application.DAL.Interfaces;
using Booking.Application.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DAL.Abstracts
{
    public abstract class Entity<T> : IEntity<T>
    {
        [Key]
        public T Id { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        [Required]
        [MaxLength(64)]
        public int CreatedById { get; set; } = 1;
        public DateTime Updated { get; set; } = DateTime.Now;
        [Required]
        [MaxLength(64)]
        public int UpdatedById { get; set; } = 1;
        public virtual User CreatedBy { get; set; }
        public virtual User UpdatedBy { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
