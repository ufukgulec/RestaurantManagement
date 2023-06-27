using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Application;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Shared.CustomExceptions;

namespace RestaurantManagement.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        public CategoryController(IUnitOfWork service) : base(service)
        {
        }
        [HttpGet("GetById/{id}")]
        public async Task<Category> GetById(string id)
        {
            return await service.CategoryRepository.GetByIdAsync(id);
        }

        [ResponseCache(Duration = 30)]
        [HttpGet("GetList")]
        public async Task<List<Category>> GetList()
        {
            return await service.CategoryRepository.GetListAsync(default, false);
        }

        [ResponseCache(Duration = 10)]
        [HttpGet("BestSeller/{filter}")]
        public async Task<IActionResult> BestSeller(string filter)
        {
            DateTime dt = DateTime.Now;

            var data = service.OrderDetailRepository.Table.AsQueryable().AsNoTracking().Where(x => x.Active);

            if (filter.ToLower() == "day")
                data = data.Where(x => x.CreatedDate > new DateTime(dt.Year, dt.Month, dt.Day));
            else if (filter.ToLower() == "month")
                data = data.Where(x => x.CreatedDate > new DateTime(dt.Year, dt.Month, 1));
            else if (filter.ToLower() == "year")
                data = data.Where(x => x.CreatedDate > new DateTime(dt.Year, 1, 1));

            var datas = await data.Include(x => x.Product).Where(x => x.Product.Active && x.Product.Category.Active).GroupBy(x => x.Product.Category.Name)
                                      .Select(x => new
                                      {
                                          Category = x.Key,
                                          Count = x.Count()
                                      }).OrderByDescending(x => x.Count).ToListAsync();

            if (datas is null)
                throw new ApiException("Kayıt Bulunamadı");

            return Ok(datas);

        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpPost("Add")]
        public async Task<bool> Add(Category? entity)
        {
            if (entity is null)
                throw new Exception("Eklenecek kategori göndermelisiniz.");

            var exist = await service.CategoryRepository.GetSingleAsync(x => x.Name.ToLower() == entity.Name.ToLower());

            if (exist is not null)
                throw new Exception("Eklemeye çalıştığınız kategorinin ismiyle bir tane daha kategori vardır.");

            var result = await service.CategoryRepository.AddAsync(entity);
            if (!result)
                throw new Exception("Eklerken bir hata oluştu.");

            return true;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpPost("Update")]
        public async Task<bool> Update(Category? entity)
        {
            return await service.CategoryRepository.Update(entity);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpDelete("Remove/{id}")]
        public async Task<bool> Remove(string id)
        {
            return await service.CategoryRepository.Remove(id);
        }
    }
}
