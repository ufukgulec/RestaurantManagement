using RestaurantManagement.Application.Repositories;
using RestaurantManagement.Domain.AppEntities;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.AppRepositories
{
    public class OSHeaderActionRepository : GenericRepository<OSHeaderAction>, IOSHeaderActionRepository
    {
        public OSHeaderActionRepository(ManagementContext context) : base(context)
        {
        }
    }
}
