using RestaurantManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Domain.AppEntities
{
    public class SubMenu : BaseEntity
    {
        public string Caption { get; set; }
        public int RowNumber { get; set; }

        public int ClickCounter { get; set; }

        public string Link { get; set; }

        public Guid TopMenuId { get; set; }
        public TopMenu? TopMenu { get; set; }
    }
}
