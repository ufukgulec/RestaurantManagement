using RestaurantManagement.Application;
using RestaurantManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.TaskForm
{
    public class Order
    {
        protected IUnitOfWork service;

        public Order(IUnitOfWork service)
        {
            this.service = service;
        }
        public async Task<bool> NewOrder(int count = 2)
        {
            var OrderTypeIds = await service.OrderTypeRepository.GetListAsync();
            var empIds = await service.EmployeeRepository.GetListAsync();
            var prcIds = await service.ProcessRepository.GetListAsync();

            Random random = new Random();
            for (int i = 0; i < count; i++)
            {
                int ot = random.Next(0, OrderTypeIds.Count);

                RestaurantManagement.Domain.Entities.Order order = new RestaurantManagement.Domain.Entities.Order
                {
                    Active = true,
                    CreatedDate = new DateTime(random.Next(2020, DateTime.Now.Year), random.Next(1, 12), random.Next(1, 30)),
                    UpdatedDate = DateTime.Now,
                    EmployeeId = empIds[random.Next(0, empIds.Count)].Id,
                    OrderTypeId = OrderTypeIds[ot].Id,
                    ProcessId = prcIds[random.Next(0, prcIds.Count)].Id,
                    Name = DateTime.Now.ToString("ddMM") + "-SPR" + OrderTypeIds[ot].Name[0].ToString().ToUpper() + "-" + i.ToString().PadLeft(4, '0'),
                };
                await service.OrderRepository.AddAsync(order);

            }
            var ordIds = await service.OrderRepository.GetListAsync(x => x.CreatedDate > DateTime.Now.Date, true);
            var prdIds = await service.ProductRepository.GetListAsync();

            for (int i = 0; i < count * 2; i++)
            {
                int ot = random.Next(0, ordIds.Count);

                OrderDetail orderDetail = new OrderDetail
                {
                    Active = true,
                    OrderId = ordIds[ot].Id,
                    CreatedDate = ordIds[ot].CreatedDate,
                    UpdatedDate = ordIds[ot].UpdatedDate,
                    ProductId = prdIds[random.Next(0, prdIds.Count)].Id,
                    Description = "Deneme"

                };
                await service.OrderDetailRepository.AddAsync(orderDetail);
            }

            return true;
        }
    }
}
