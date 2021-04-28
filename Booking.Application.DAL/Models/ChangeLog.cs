using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DAL.Models
{
    public partial class ChangeLog
    {
        public long ChangeLogID { get; set; }
        public long ChangeEventID { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string RowID { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string Operation { get; set; } = "UPDATE";
        public virtual ChangeEvent ChangeEvent { get; set; }
    }
}
