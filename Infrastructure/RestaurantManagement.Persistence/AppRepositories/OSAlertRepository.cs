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
    public class OSAlertRepository : GenericRepository<OSAlert>, IOSAlertRepository
    {
        public OSAlertRepository(ManagementContext context) : base(context)
        {
        }
    }
}
