using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.UI.Utils.Services.Interfaces
{
    public interface INavigationMenuService : IBaseService
    {
        Task<bool> AddAsync(Navigation entity);
        Task<List<Navigation>> GetListAsync();
        Task<bool> RemoveAsync(string id);
        Task<bool> UpdateAsync(Navigation entity);
    }
}