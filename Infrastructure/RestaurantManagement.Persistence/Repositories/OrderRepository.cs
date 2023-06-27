using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        ManagementContext _Context;

        public OrderRepository(ManagementContext context) : base(context)
        {
            _Context = context;
        }


        /// <summary>
        /// Sipariş oluşturmak için gerekli şeyler
        /// OrderTypeId → Masa Siparişi,Rezervasyon,Gel Al Siparişi,Telefon Siparişi
        /// EmployeeId → Giriş yapan kullanıcı idsi
        /// ProcessId → Oluşturuldu process tablosunda key lazım
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<Order> CreateOrderAsync(Order order)
        {
            order.Name = $"{DateTime.Now.ToString("ddMM")}-SPR{order.OrderType.Name[0].ToString().ToUpper()}-{Table.Count().ToString().PadLeft(4, '0')}";
            Table.Add(order);
            throw new NotImplementedException();
        }
    }
}
