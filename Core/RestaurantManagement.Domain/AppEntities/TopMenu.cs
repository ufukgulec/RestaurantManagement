using RestaurantManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Domain.AppEntities
{
    public class TopMenu : BaseEntity
    {
        public string Caption { get; set; }
        public int RowNumber { get; set; }

        public string Icon { get; set; }

        public Guid MenuSectionId { get; set; }
        public MenuSection? MenuSection { get; set; }

        public ICollection<SubMenu>? SubMenus { get; set; }
    }
}
