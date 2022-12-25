using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Application;
using RestaurantManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Console
{

    public class OrderCounts
    {
        protected readonly IUnitOfWork service;
        public enum Filter { d, m, y };
        public OrderCounts(IUnitOfWork service)
        {
            this.service = service;
        }
        public async Task<object> CategoryOrder(Filter filter)
        {

            //var datas = (from c in await service.CategoryRepository.GetListAsync(default, false)
            //             join p in await service.ProductRepository.GetListAsync(default, false) on c.Id equals p.CategoryId
            //             join o in await service.OrderDetailRepository.GetListAsync(default, false) on p.Id equals o.ProductId
            //             group c by c.Name into g
            //             select new
            //             {
            //                 name = g.Key,
            //                 count = g.Count()
            //             }).ToList();

            switch (filter)
            {
                case Filter.d:
                    break;
                case Filter.m:
                    break;
                case Filter.y:
                    break;
                default:
                    break;
            }
            var data = service.OrderDetailRepository
                .GetAll(default, false, false, x => x.Product)
                    .GroupBy(x => x.Product.Category.Name)
                        .Select(x => new
                        {
                            name = x.Key,
                            count = x.Count()
                        })
                        .ToList();
            return data;
        }
    }
}
