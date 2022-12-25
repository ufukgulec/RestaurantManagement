﻿using RestaurantManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Domain.Dto
{
    public class TopSellingCategory
    {
        public Category Category { get; set; }
        public int Count { get; set; }
    }
}
