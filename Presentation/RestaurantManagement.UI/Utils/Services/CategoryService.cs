using Radzen;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.UI.Utils.Extensions;
using RestaurantManagement.UI.Utils.Managers.Interfaces;
using RestaurantManagement.UI.Utils.Services.Interfaces;
using System;
using System.Net.Http;

namespace RestaurantManagement.UI.Utils.Services
{
    public class CategoryService : BaseService, ICategoryService
    {
        public CategoryService(HttpClient httpClient, INotificationManager notificationManager) : base(httpClient, notificationManager)
        {

        }

        public async Task<List<Category>> GetById(string id)
        {
            var ResponseResult = await Client.GetServiceResponseAsync<List<Category>>($"/api/Category/GetById/{id}");

            return ResponseResult;
        }

        public async Task<List<Category>> GetListAsync()
        {
            var ResponseResult = await Client.GetServiceResponseAsync<List<Category>>($"/api/Category/GetList");
            return ResponseResult;
        }

        public async Task<IEnumerable<Category>> GetAllAsync(string filter = default(string), int? top = default(int?), int? skip = default(int?), string orderby = default(string), string expand = default(string), string select = default(string), bool? count = default(bool?))
        {
            var uri = new Uri("https://localhost:5001/api/category/GetAll");
            uri = uri.GetODataUri(filter: filter, top: top, skip: skip, orderby: orderby, expand: expand, select: select, count: count);

            var data = await Client.GetServiceResponseAsync<IEnumerable<Category>>(uri);
            int count1 = data.Count();
            return data;
        }

        public async Task<List<Category>> BestSellerAsync(string filter)
        {
            var ResponseResult = await Client.GetServiceResponseAsync<List<Category>>($"/api/Category/BestSeller/{filter}");

            return ResponseResult;
        }

        public async Task<bool> AddAsync(Category entity)
        {
            return true;
        }
        public async Task<bool> UpdateAsync(Category entity)
        {
            return true;
        }
        public async Task<bool> RemoveAsync(string id)
        {
            return true;
        }
    }
}
