using RestaurantManagement.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository CategoryRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        IIngredientRepository IngredientRepository { get; }
        IOrderRepository OrderRepository { get; }
        IOrderDetailRepository OrderDetailRepository { get; }
        IOrderTypeRepository OrderTypeRepository { get; }
        IProcessRepository ProcessRepository { get; }
        IProductRepository ProductRepository { get; }
        IRecipeRepository RecipeRepository { get; }
        IRoleRepository RoleRepository { get; }
        ISupplierRepository SupplierRepository { get; }
        IWholesaleRepository WholesaleRepository { get; }
        INotificationTypeRepository NotificationTypeRepository { get; }
        INotificationRepository NotificationRepository { get; }

        #region OS
        IMenuRepository MenuRepository { get; }
        IOSAlertRepository OSAlertRepository{get;}
        IOSEventRepository OSEventRepository {get;}
        IOSHeaderActionRepository OSHeaderActionRepository {get;}
        IOSHeaderRepository OSHeaderRepository {get;}
        IOSLogRepository OSLogRepository {get;}
        #endregion


        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
