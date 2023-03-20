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
    public class MenuRepository : GenericRepository<Menu>, IMenuRepository
    {
        private readonly ManagementContext _context;
        public MenuRepository(ManagementContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> DeleteAll()
        {
            _context.RemoveRange(_context.Menus);

            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UpdateOrAdd(List<Menu> links)
        {
            foreach (var link in links)
            {
                var entity = GetById(link.Id.ToString());

                if (entity is null)
                {
                    await AddAsync(link);
                }
                else
                {
                    await Update(link);
                }
            }
            return true;
        }
    }
}
