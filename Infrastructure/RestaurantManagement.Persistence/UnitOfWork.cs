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
        public ICategoryRepository? _CategoryRepository;
        public IEmployeeRepository? _EmployeeRepository;
        public IIngredientRepository? _IngredientRepository;
        public IOrderRepository? _OrderRepository;
        public IOrderDetailRepository? _OrderDetailRepository;
        public IOrderTypeRepository? _OrderTypeRepository;
        public IProcessRepository? _ProcessRepository;
        public IProductRepository? _ProductRepository;
        public IRecipeRepository? _RecipeRepository;
        public IRoleRepository? _RoleRepository;
        public ISupplierRepository? _SupplierRepository;
        public IWholesaleRepository? _WholesaleRepository;

        public ISectionRepository? _SectionRepository;
        public ITopMenuRepository? _TopMenuRepository;
        public ISubMenuRepository? _SubMenuRepository;


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

        public ISectionRepository SectionRepository => _SectionRepository ?? (_SectionRepository = new SectionRepository(context));

        public ITopMenuRepository TopMenuRepository => _TopMenuRepository ?? (_TopMenuRepository = new TopMenuRepository(context));

        public ISubMenuRepository SubMenuRepository => _SubMenuRepository ?? (_SubMenuRepository = new SubMenuRepository(context));

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
