using RestaurantManagement.Domain.AppEntities;
using RestaurantManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Repositories
{
    public interface IMenuRepository : IGenericRepository<Menu>
    {
        public Task<bool> UpdateOrAdd(List<Menu> links);
        public Task<bool> DeleteAll();
    }
}
