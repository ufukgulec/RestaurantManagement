using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Persistence.Contexts;
using System.Linq.Expressions;

namespace RestaurantManagement.Application.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ManagementContext _context;

        public GenericRepository(ManagementContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();
        #region Select
        #region No Async
        public T? GetById(string id, params Expression<Func<T, object>>[] includes)
        {
            var query = Table.AsQueryable();
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
            var entity = query.FirstOrDefault(data => data.Id == Guid.Parse(id));

            if (entity is null)
                throw new ArgumentException($"UniqueId : {id} Bu nesne '{typeof(T).Name}' tablosunda bulunamadı.");

            return entity;
        }
        public T? GetSingle(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            var query = Table.AsQueryable().AsNoTracking();

            foreach (var item in includes)
            {
                query = query.Include(item);
            }
            var entity = query.FirstOrDefault(expression);

            if (entity is null)
                throw new ArgumentException($"'{typeof(T).Name}' tablosunda aradığınız nesne bulunamadı.");

            return entity;
        }

        public List<T> GetList(Expression<Func<T, bool>>? expression = null, bool justActive = true, params Expression<Func<T, object>>[] includes)
        {
            var query = Table.AsQueryable().AsNoTracking();

            if (justActive)
            {
                query = query.Where(x => x.Active);
            }
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
            if (expression is not null)
            {
                query = query.Where(expression);
            }

            if (query is null)
                throw new ArgumentException($"'{typeof(T).Name}' tablosunda kayıt bulunamadı.");

            return query.ToList();
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>>? expression = null, bool justActive = true, bool tracking = true, params Expression<Func<T, object>>[] includes)
        {
            var query = Table.AsQueryable();
            if (!tracking)
            {
                query = query.AsNoTracking();
            }
            if (justActive)
            {
                query = query.Where(x => x.Active);
            }
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
            if (expression is not null)
            {
                query = query.Where(expression);
            }

            if (query is null)
                throw new ArgumentException($"'{typeof(T).Name}' tablosunda kayıt bulunamadı.");

            return query;
        }
        #endregion

        #region Async
        public async Task<T?> GetByIdAsync(string id, bool tracking = true, params Expression<Func<T, object>>[] includes)
        {
            var query = Table.AsQueryable();
            if (!tracking)
            {
                query = query.AsNoTracking();
            }
            foreach (var item in includes)
            {
                query = query.Include(item);
            }

            var entity = await query.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));

            if (entity is null)
                throw new ArgumentException($"UniqueId : {id} Bu nesne '{typeof(T).Name}' tablosunda bulunamadı.");

            return entity;

        }
        public async Task<T?> GetSingleAsync(Expression<Func<T, bool>>? expression = null, bool tracking = true, params Expression<Func<T, object>>[] includes)
        {
            var query = Table.AsQueryable();
            if (!tracking)
            {
                query = query.AsNoTracking();
            }
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
            if (expression is not null)
            {
                return await query.FirstOrDefaultAsync(expression);
            }
            else
            {
                return await query.FirstOrDefaultAsync();
            }

        }

        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>>? expression = null, bool justActive = true, params Expression<Func<T, object>>[] includes)
        {
            var query = Table.AsNoTracking();
            if (justActive)
            {
                query = query.Where(x => x.Active);
            }

            foreach (var item in includes)
            {
                query = query.Include(item);
            }
            if (expression is not null)
            {
                query = query.Where(expression);
            }

            if (query is null)
                throw new ArgumentException($"'{typeof(T).Name}' tablosunda kayıt bulunamadı.");

            return await query.ToListAsync();
        }

        #endregion
        #endregion
        #region CRUD
        public async Task<bool> AddAsync(T entity)
        {
            EntityEntry<T> entityEntry = await Table.AddAsync(entity);

            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> AddRangeAsync(List<T> entities)
        {
            await Table.AddRangeAsync(entities);

            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> Update(T entity)
        {
            var exist = await GetByIdAsync(entity.Id.ToString());

            EntityEntry<T> entityEntry = Table.Update(entity);

            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> Remove(string id)
        {
            var entity = await GetByIdAsync(id);

            return await Remove(entity);
        }

        public async Task<bool> Remove(T entity)
        {
            if (entity.Active)
            {
                entity.Active = false;
                return await Update(entity);
            }
            else
            {
                EntityEntry<T> entityEntry = Table.Remove(entity);
                if (await _context.SaveChangesAsync() > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public bool RemoveRange(List<T> entities)
        {
            Table.RemoveRange(entities);
            //Save();
            return true;
        }

        public bool BulkDeleteById(List<string> ids)
        {
            foreach (var id in ids)
            {
                // Remove(id);
            }
            return true;
        }

        public bool BulkDeleteById(Expression<Func<T, bool>> expression)
        {
            var data = GetList(expression);
            return RemoveRange(data);

        }

        public IQueryable<T> FromSql(FormattableString sql)
        {
            return _context.Set<T>().FromSql<T>(sql);
        }

        public bool Attach(T entity)
        {
            return true;
        }

        public async Task<bool> AttachRange(List<T> entities)
        {
            Table.AttachRange(entities);
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

        #endregion
    }
}
