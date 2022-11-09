using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Guid OrderTypeId { get; set; }
        public OrderType? OrderType { get; set; }

        public Guid EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        public Guid ProcessId { get; set; }
        public Process? Process { get; set; }

        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
