using RestaurantManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        /*
         * Sipariş oluşturmak için gerekli şeyler
         * OrderTypeId → Masa Siparişi,Rezervasyon,Gel Al Siparişi,Telefon Siparişi
         * EmployeeId → Giriş yapan kullanıcı idsi
         * ProcessId → Oluşturuldu process tablosunda key lazım
         * 
         * Oluşan Order ile OrderDetail tablosuna kayıt atılacak
         * Seçilen ürünleri bir listede tutup OrderId ile kayıt atılacak
         */

        Task<Order> CreateOrderAsync(Order order);
    }
}
