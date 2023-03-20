using RestaurantManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Domain.AppEntities
{
    public class Menu : BaseEntity
    {
        public string Row_ID { get; set; }
        public string Head_ID { get; set; }
        public string Caption { get; set; }
        public string? Controller { get; set; }
        public int Row_Number { get; set; }
        public string? Action { get; set; }
        public string? Params { get; set; }
        public string? Icon { get; set; }
        public string? Type { get; set; }
    }
}
