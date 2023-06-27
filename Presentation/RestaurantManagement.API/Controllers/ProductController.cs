using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RestaurantManagement.Application;
using RestaurantManagement.Application.Repositories;
using RestaurantManagement.Domain.Entities;
using System;

namespace RestaurantManagement.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {

        public ProductController(IUnitOfWork service) : base(service)
        {

        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await service.ProductRepository.GetByIdAsync(id);

            if (result is not null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("GetListByCategoryId/{categoryId}")]
        public async Task<IActionResult> GetListByCategoryId(string categoryId)
        {
            var result = await service.ProductRepository.GetListAsync(p => p.CategoryId.ToString() == categoryId);

            if (result.Count > 0)
            {
                return Ok(result);
            }
            return new NotFoundResult();
        }
        [ResponseCache(Duration = 10)]
        [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {
            var result = await service.ProductRepository.GetListAsync(default, false);

            if (result is not null)
            {
                return Ok(result);
            }
            return BadRequest();
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
            if (datas is not null)
            {
                return Ok(datas);
            }
            return BadRequest();
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpPost("Add")]
        public async Task<IActionResult> Add(Product? entity)
        {
            var result = false;
            var Message = "";
            if (entity != null)
            {
                var exist = await service.ProductRepository.GetSingleAsync(x => x.Name.ToLower() == entity.Name.ToLower());

                if (exist == null)
                {
                    result = await service.ProductRepository.AddAsync(entity);
                    if (result)
                    {
                        Message = "Başarılı";
                    }
                    else
                    {
                        Message = "Eklerken bir hata oluştu";
                    }
                }
                else
                {
                    Message = "Eklemeye çalıştığınız kategorinin ismiyle bir tane daha kategori vardır.";
                }
            }
            if (result)
            {
                return Ok(Message);
            }
            else
            {
                return BadRequest(Message);
            }

        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpPost("Update")]
        public async Task<IActionResult> Update(Product? entity)
        {
            var result = false;
            var Message = "";
            if (entity != null)
            {
                var exist = await service.ProductRepository.GetByIdAsync(entity.Id.ToString(), false);

                if (exist != null)
                {
                    result = await service.ProductRepository.Update(entity);
                    if (result)
                    {
                        Message = "Başarılı";
                    }
                    else
                    {
                        Message = "Güncellerken bir hata oluştu";
                    }
                }
                else
                {
                    Message = "Güncellerken çalıştığınız kategori bulunamadı.";
                }
            }
            if (result)
            {
                return Ok(Message);
            }
            else
            {
                return BadRequest(Message);
            }

        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpDelete("Remove/{id}")]
        public async Task<IActionResult> Remove(string id)
        {
            var result = false;
            var Message = "";
            var exist = await service.ProductRepository.GetByIdAsync(id);

            if (exist != null)
            {
                if (exist.Active)
                {
                    exist.Active = false;
                    await service.ProductRepository.Update(exist);
                    Message = "Kategori Pasif duruma getirildi.";
                }
                else
                {
                    result = await service.ProductRepository.Remove(id);
                    if (result)
                    {
                        Message = "Başarılı";
                    }
                    else
                    {
                        Message = "Silerken bir hata oluştu";
                    }
                }

            }
            else
            {
                Message = "Silmeye çalıştığınız kategori bulunamadı";
            }

            if (result)
            {
                return Ok(Message);
            }
            else
            {
                return BadRequest(Message);
            }
        }
    }
}
