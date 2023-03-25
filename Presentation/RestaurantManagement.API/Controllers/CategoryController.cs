﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RestaurantManagement.Application;
using RestaurantManagement.Application.Repositories;
using RestaurantManagement.Domain.Entities;
using System;

namespace RestaurantManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {

        public CategoryController(IUnitOfWork service) : base(service)
        {

        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await service.CategoryRepository.GetByIdAsync(id);

            if (result is not null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {
            var result = await service.CategoryRepository.GetListAsync(default, false);

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
        public async Task<IActionResult> Add(Category? entity)
        {
            var result = false;
            var Message = "";
            if (entity != null)
            {
                var exist = await service.CategoryRepository.GetSingleAsync(x => x.Name.ToLower() == entity.Name.ToLower());

                if (exist == null)
                {
                    result = await service.CategoryRepository.AddAsync(entity);
                    if (result)
                    {
                        Message += "Başarılı";
                    }
                    else
                    {
                        Message += "Eklerken bir hata oluştu";
                    }
                }
                else
                {
                    Message += "Eklemeye çalıştığınız kategorinin ismiyle bir tane daha kategori vardır.";
                }
            }
            if (result)
            {
                return Ok("Başarılı");
            }
            else
            {
                return BadRequest(Message);
            }

        }
        [HttpDelete("Remove/{id}")]
        public async Task<IActionResult> Remove(string id)
        {

            var data = await service.CategoryRepository.GetByIdAsync("");
            return Ok();
        }
    }
}
