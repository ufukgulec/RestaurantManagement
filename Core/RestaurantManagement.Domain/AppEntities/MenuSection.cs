﻿using RestaurantManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Domain.AppEntities
{
    public class MenuSection : BaseEntity
    {
        public string Caption { get; set; }
        public int RowNumber { get; set; }

        public ICollection<TopMenu>? TopMenus { get; set; }
    }
}
