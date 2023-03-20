using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Domain.Entities;
using System.Linq.Expressions;

namespace RestaurantManagement.API.Controllers
{
    public interface IController<T> where T : BaseEntity
    {
        public Task<IActionResult> GetById(string id);

        public Task<IActionResult> GetList();

        public Task<IActionResult> GetOrderListAsync(string columnName, string orderType);
    }
}
