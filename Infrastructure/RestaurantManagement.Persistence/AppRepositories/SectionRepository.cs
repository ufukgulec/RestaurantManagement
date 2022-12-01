using Microsoft.EntityFrameworkCore;
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
    public class SectionRepository : GenericRepository<MenuSection>, ISectionRepository
    {
        public SectionRepository(ManagementContext context) : base(context)
        {

        }

        public List<MenuSection> GetNavs()
        {
            var data = Table.AsNoTracking()
                .Include(section => section.TopMenus
                    .Where(tm => tm.Active && tm.SubMenus.Count > 0)
                    .OrderBy(x => x.RowNumber))
                .ThenInclude(topmenu => topmenu.SubMenus
                    .Where(sm => sm.Active)
                    .OrderBy(x => x.RowNumber))
                .Where(section => section.Active && section.TopMenus.Count > 0)
                .OrderBy(x => x.RowNumber)
                .ToList();
            return data;
        }
    }
}
