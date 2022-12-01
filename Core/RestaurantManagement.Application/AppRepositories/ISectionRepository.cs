using RestaurantManagement.Domain.AppEntities;
using RestaurantManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Repositories
{
    public interface ISectionRepository : IGenericRepository<MenuSection>
    {
        List<MenuSection> GetNavs();
    }
}
