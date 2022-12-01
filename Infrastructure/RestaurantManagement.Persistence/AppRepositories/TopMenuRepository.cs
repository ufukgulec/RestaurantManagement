using RestaurantManagement.Domain.AppEntities;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Repositories
{
    public class TopMenuRepository : GenericRepository<TopMenu>, ITopMenuRepository
    {
        public TopMenuRepository(ManagementContext context) : base(context)
        {
        }
    }
}
