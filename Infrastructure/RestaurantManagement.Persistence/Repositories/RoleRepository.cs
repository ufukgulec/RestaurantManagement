﻿using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(ManagementContext context) : base(context)
        {
        }
    }
}
