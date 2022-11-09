using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Domain.Entities
{
    public class Process : BaseEntity
    {
        public string Name { get; set; }
        public string IconUrl { get; set; }

        public ICollection<Order>? Orders { get; set; }
    }
}
