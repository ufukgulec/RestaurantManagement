using RestaurantManagement.Domain.Dto;
using RestaurantManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Repositories
{
    public interface IOrderDetailRepository : IGenericRepository<OrderDetail>
    {
        public enum Filter { day, month, year, all };
        public Task<object> GetCategoryRelatedInformation(Filter filter);
        public List<TopSellingCategory> GetTopSellingCategories();
    }

}
