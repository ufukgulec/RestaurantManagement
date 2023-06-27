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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : BaseController
    {

        public OrderDetailController(IUnitOfWork service) : base(service)
        {

        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await service.OrderDetailRepository.GetByIdAsync(id);

            if (result is not null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {
            var result = await service.OrderDetailRepository.GetListAsync(default, false);

            if (result is not null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

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
        [HttpPost("Add")]
        public async Task<IActionResult> Add(OrderDetail? entity)
        {
            var result = false;
            var Message = "";
            if (entity != null)
            {

                result = await service.OrderDetailRepository.AddAsync(entity);
                if (result)
                {
                    Message = "Başarılı";
                }
                else
                {
                    Message = "Eklerken bir hata oluştu";
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

        [HttpPost("Update")]
        public async Task<IActionResult> Update(OrderDetail? entity)
        {
            var result = false;
            var Message = "";
            if (entity != null)
            {
                var exist = await service.OrderDetailRepository.GetByIdAsync(entity.Id.ToString(), false);

                if (exist != null)
                {
                    result = await service.OrderDetailRepository.Update(entity);
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
        [HttpDelete("Remove/{id}")]
        public async Task<IActionResult> Remove(string id)
        {
            var result = false;
            var Message = "";
            var exist = await service.OrderDetailRepository.GetByIdAsync(id);

            if (exist != null)
            {
                if (exist.Active)
                {
                    exist.Active = false;
                    await service.OrderDetailRepository.Update(exist);
                    Message = "Kategori Pasif duruma getirildi.";
                }
                else
                {
                    result = await service.OrderDetailRepository.Remove(id);
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
