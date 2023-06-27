using RestaurantManagement.Application.Repositories;
using RestaurantManagement.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposedValue;

        ManagementContext context;

        public UnitOfWork(ManagementContext context)
        {
            this.context = context;
        }
        //Fields
        private ICategoryRepository? _CategoryRepository;
        private IEmployeeRepository? _EmployeeRepository;
        private IIngredientRepository? _IngredientRepository;
        private IOrderRepository? _OrderRepository;
        private IOrderDetailRepository? _OrderDetailRepository;
        private IOrderTypeRepository? _OrderTypeRepository;
        private IProcessRepository? _ProcessRepository;
        private IProductRepository? _ProductRepository;
        private IRecipeRepository? _RecipeRepository;
        private IRoleRepository? _RoleRepository;
        private ISupplierRepository? _SupplierRepository;
        private IWholesaleRepository? _WholesaleRepository;
        private INotificationTypeRepository? _NotificationTypeRepository;
        private INotificationRepository? _NotificationRepository;
        private INavigationRepository? _NavigationRepository;


        public ICategoryRepository CategoryRepository => _CategoryRepository ?? (_CategoryRepository = new CategoryRepository(context));

        public IEmployeeRepository EmployeeRepository => _EmployeeRepository ?? (_EmployeeRepository = new EmployeeRepository(context));

        public IIngredientRepository IngredientRepository => _IngredientRepository ?? (_IngredientRepository = new IngredientRepository(context));

        public IOrderRepository OrderRepository => _OrderRepository ?? (_OrderRepository = new OrderRepository(context));

        public IOrderDetailRepository OrderDetailRepository => _OrderDetailRepository ?? (_OrderDetailRepository = new OrderDetailRepository(context));

        public IOrderTypeRepository OrderTypeRepository => _OrderTypeRepository ?? (_OrderTypeRepository = new OrderTypeRepository(context));

        public IProcessRepository ProcessRepository => _ProcessRepository ?? (_ProcessRepository = new ProcessRepository(context));

        public IProductRepository ProductRepository => _ProductRepository ?? (_ProductRepository = new ProductRepository(context));

        public IRecipeRepository RecipeRepository => _RecipeRepository ?? (_RecipeRepository = new RecipeRepository(context));

        public IRoleRepository RoleRepository => _RoleRepository ?? (_RoleRepository = new RoleRepository(context));

        public ISupplierRepository SupplierRepository => _SupplierRepository ?? (_SupplierRepository = new SupplierRepository(context));

        public IWholesaleRepository WholesaleRepository => _WholesaleRepository ?? (_WholesaleRepository = new WholesaleRepository(context));

        public INotificationTypeRepository NotificationTypeRepository => _NotificationTypeRepository ?? (_NotificationTypeRepository = new NotificationTypeRepository(context));

        public INotificationRepository NotificationRepository => _NotificationRepository ?? (_NotificationRepository = new NotificationRepository(context));

        public INavigationRepository NavigationRepository => _NavigationRepository ?? (_NavigationRepository = new NavigationRepository(context));


        public int SaveChanges()
        {
            using (var dbTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    dbTransaction.Commit();
                    return context.SaveChanges();
                }
                catch (Exception)
                {
                    dbTransaction?.Rollback();
                    return 0;
                }
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            using (var dbTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    dbTransaction.Commit();
                    return await context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    dbTransaction?.Rollback();
                    return 0;
                }
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: yönetilen durumu (yönetilen nesneleri) atın
                }

                // TODO: yönetilmeyen kaynakları (yönetilmeyen nesneleri) serbest bırakın ve sonlandırıcıyı geçersiz kılın
                // TODO: büyük alanları null olarak ayarlayın
                disposedValue = true;
            }
        }

        // // TODO: sonlandırıcıyı yalnızca 'Dispose(bool disposing)' içinde yönetilmeyen kaynakları serbest bırakacak kod varsa geçersiz kılın
        // ~UnitOfWork()
        // {
        //     // Bu kodu değiştirmeyin. Temizleme kodunu 'Dispose(bool disposing)' metodunun içine yerleştirin.
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Bu kodu değiştirmeyin. Temizleme kodunu 'Dispose(bool disposing)' metodunun içine yerleştirin.
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
