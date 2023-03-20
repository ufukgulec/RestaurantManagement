using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        DbSet<T> Table { get; }
        #region Select

        /// <summary>
        /// Id'ye göre veri döner. Include işlemi için (x=>x.Property1,x=>x.Property2) kullanılmalıdır.
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="includes"></param>
        /// <returns>Tekil veri T</returns>
        T? GetById(string id, params Expression<Func<T, object>>[] includes);
        /// <summary>
        /// Girilen koşula göre tekil veri döner. Include işlemi için (x=>x.Property1,x=>x.Property2) kullanılmalıdır.
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        T? GetSingle(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
        /// <summary>
        /// Girilebilecek koşula göre veri döner. Eğer koşul yoksa null atanmalıdır. Include işlemi için (x=>x.Property1,x=>x.Property2) kullanılmalıdır.
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="includes"></param>
        /// <returns>Çoğul veri List T</returns>
        List<T> GetList(Expression<Func<T, bool>>? expression = null, bool justActive = true, params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Girilebilecek koşula göre veri döner. Eğer koşul yoksa null atanmalıdır. Tracking mekanizmasına ihtiyaç yoksa false atanmalıdı. Include işlemi için (x=>x.Property1,x=>x.Property2) kullanılmalıdır.
        /// </summary>
        /// <param name="expression">Koşul</param>
        /// <param name="tracking">Tracking</param>
        /// <param name="includes">Include</param>
        /// <returns>Çoğul veri IQueryable T</returns>
        IQueryable<T> GetAll(Expression<Func<T, bool>>? expression = null, bool justActive = true, bool tracking = true, params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Asenkron - Id'ye göre veriyi verir. Tracking mekanizmasına ihtiyaç yoksa false atanmalıdı. Include işlemi için (x=>x.Property1,x=>x.Property2) kullanılmalıdır.
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="tracking">Tracking</param>
        /// <param name="includes">Include</param>
        /// <returns>Tekil veri Task T</returns>
        Task<T?> GetByIdAsync(string id, bool tracking = true, params Expression<Func<T, object>>[] includes);
        /// <summary>
        /// Girilen koşula göre tekil veri döner. Tracking mekanizmasına ihtiyaç yoksa false atanmalıdı. Include işlemi için (x=>x.Property1,x=>x.Property2) kullanılmalıdır.
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="tracking">Tracking</param>
        /// <param name="includes">Include</param>
        /// <returns>Tekil veri Task T</returns>
        Task<T?> GetSingleAsync(Expression<Func<T, bool>>? expression = null, bool tracking = true, params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<List<T>> GetListAsync(Expression<Func<T, bool>>? expression = null, bool justActive = true, params Expression<Func<T, object>>[] includes);
        #endregion
        #region CRUD
        Task<bool> AddAsync(T entity);
        Task<bool> AddRangeAsync(List<T> entities);
        Task<bool> Remove(string id);
        Task<bool> Remove(T entity);
        bool RemoveRange(List<T> entities);
        Task<bool> Update(T entity);
        bool Attach(T entity);
        Task<bool> AttachRange(List<T> entities);

        bool BulkDeleteById(List<string> ids);
        bool BulkDeleteById(Expression<Func<T, bool>> expression);
        #endregion
        #region FromSql

        IQueryable<T> FromSql(FormattableString sql);
        #endregion
    }
}
