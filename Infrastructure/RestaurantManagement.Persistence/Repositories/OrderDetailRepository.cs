using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Domain.Dto;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RestaurantManagement.Application.Repositories.IOrderDetailRepository;

namespace RestaurantManagement.Application.Repositories
{
    public class OrderDetailRepository : GenericRepository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(ManagementContext context) : base(context)
        {

        }
        public async Task<object> GetCategoryRelatedInformation(IOrderDetailRepository.Filter filter)
        {
            DateTime dt = DateTime.Now;

            var data = Table.AsQueryable().AsNoTracking();

            if (filter == IOrderDetailRepository.Filter.day)
                data = data.Where(x => x.CreatedDate > new DateTime(dt.Year, dt.Month, dt.Day));
            else if (filter == IOrderDetailRepository.Filter.month)
                data = data.Where(x => x.CreatedDate > new DateTime(dt.Year, dt.Month, 1));
            else if (filter == IOrderDetailRepository.Filter.year)
                data = data.Where(x => x.CreatedDate > new DateTime(dt.Year, 1, 1));

            var datas = await data.Include(x => x.Product).GroupBy(x => x.Product.Category.Name)
                                      .Select(x => new
                                      {
                                          arg = x.Key,
                                          val = x.Count()
                                      }).ToListAsync();

            return datas;
        }

        public List<TopSellingCategory> GetTopSellingCategories()
        {
            var data = Table.AsQueryable().AsNoTracking()
                .Include(x => x.Product).Where(x => x.CreatedDate > DateTime.Now.Date)
                .GroupBy(x => x.Product.Category)
                .Select(x => new TopSellingCategory
                {
                    Category = x.Key,
                    Count = x.Count()
                }).OrderByDescending(x => x.Count).ToList();

            return data;
        }
    }
}
