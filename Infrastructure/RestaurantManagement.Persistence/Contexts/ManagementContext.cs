using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Persistence.Contexts
{
    public class ManagementContext : DbContext
    {
        public ManagementContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderType> OrderTypes { get; set; }
        public DbSet<Process> Processes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Wholesale> Wholesales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var conn = "server=UFUK;database=ManagementDB;integrated security=true;TrustServerCertificate=True;";
                optionsBuilder.UseSqlServer(conn);
            }
            //optionsBuilder.LogTo(message => Console.WriteLine(message));
        }
        //public override int SaveChanges()
        //{
        //    OnBeforeSave();
        //    return base.SaveChanges();
        //}
        //public override int SaveChanges(bool acceptAllChangesOnSuccess)
        //{
        //    OnBeforeSave();
        //    return base.SaveChanges(acceptAllChangesOnSuccess);
        //}
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            //OnBeforeSave();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSave();
            return base.SaveChangesAsync(cancellationToken);
        }
        private void OnBeforeSave()
        {
            var added = ChangeTracker.Entries()
                                    .Where(i => i.State == EntityState.Added)
                                    .Select(i => (BaseEntity)i.Entity);

            var modified = ChangeTracker.Entries()
                                    .Where(i => i.State == EntityState.Modified)
                                    .Select(i => (BaseEntity)i.Entity);
            if (added.Count()>0)
            {
                PrepareAddedEntities(added);
            }
            else if (modified.Count() > 0)
            {
                PrepareModifiedEntities(modified);
            }
            
        }
        private void PrepareAddedEntities(IEnumerable<BaseEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.CreatedDate = DateTime.Now;
                entity.UpdatedDate = DateTime.Now;
            }
        }
        private void PrepareModifiedEntities(IEnumerable<BaseEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.UpdatedDate = DateTime.Now;
            }
        }
    }
}
