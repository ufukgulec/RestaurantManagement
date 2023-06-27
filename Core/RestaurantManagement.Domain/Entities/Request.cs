using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Domain.Entities
{
    public class Request : BaseEntity
    {
        public string Path { get; set; }
        public string Method { get; set; }
        public string StatusCode { get; set; }
    }
}
