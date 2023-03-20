using RestaurantManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Domain.AppEntities
{
    public class OSEvent : BaseEntity
    {
        public string? Name { get; set; }
        public string? Action { get; set; }
        public string? Icon { get; set; }
        public string? Class { get; set; }

    }
}
