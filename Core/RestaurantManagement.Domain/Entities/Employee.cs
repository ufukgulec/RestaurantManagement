using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Domain.Entities
{
    public class Employee : BaseEntity
    {
        public string Fullname { get; set; }
        public string PhoneNumber { get; set; }

        public Guid RoleId { get; set; }
        public Role? Role { get; set; }

        public ICollection<Order>? Orders { get; set; }
    }
}
