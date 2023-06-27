using Radzen;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.UI.Utils.Services.Interfaces
{
    public interface ICategoryService : IBaseService
    {
        Task<bool> AddAsync(Category entity);
        Task<List<Category>> BestSellerAsync(string filter);
        Task<List<Category>> GetById(string id);
        Task<List<Category>> GetListAsync();
        Task<IEnumerable<Category>> GetAllAsync(string filter = default(string), int? top = default(int?), int? skip = default(int?), string orderby = default(string), string expand = default(string), string select = default(string), bool? count = default(bool?));
        Task<bool> RemoveAsync(string id);
        Task<bool> UpdateAsync(Category entity);
    }
}