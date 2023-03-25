using Microsoft.AspNetCore.Mvc;
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
    public class OrderController : BaseController
    {

        public OrderController(IUnitOfWork service) : base(service)
        {

        }

        [HttpGet("CreateRandom/{count}")]
        public async Task<IActionResult> CreateRandom(int count)
        {
            try
            {
                var OrderTypeIds = await service.OrderTypeRepository.GetListAsync();
                var empIds = await service.EmployeeRepository.GetListAsync();
                var prcIds = await service.ProcessRepository.GetListAsync();

                Random random = new Random();
                for (int i = 0; i < count; i++)
                {
                    int ot = random.Next(0, OrderTypeIds.Count);

                    RestaurantManagement.Domain.Entities.Order order = new RestaurantManagement.Domain.Entities.Order
                    {
                        Active = true,
                        CreatedDate = new DateTime(random.Next(2020, DateTime.Now.Year), random.Next(1, 12), random.Next(1, 30)),
                        UpdatedDate = DateTime.Now,
                        EmployeeId = empIds[random.Next(0, empIds.Count)].Id,
                        OrderTypeId = OrderTypeIds[ot].Id,
                        ProcessId = prcIds[random.Next(0, prcIds.Count)].Id,
                        Name = DateTime.Now.ToString("ddMM") + "-SPR" + OrderTypeIds[ot].Name[0].ToString().ToUpper() + "-" + i.ToString().PadLeft(4, '0'),
                    };
                    await service.OrderRepository.AddAsync(order);

                }
                var ordIds = await service.OrderRepository.GetListAsync(x => x.CreatedDate > DateTime.Now.Date, true);
                var prdIds = await service.ProductRepository.GetListAsync();

                for (int i = 0; i < count * 2; i++)
                {
                    int ot = random.Next(0, ordIds.Count);

                    OrderDetail orderDetail = new OrderDetail
                    {
                        Active = true,
                        OrderId = ordIds[ot].Id,
                        CreatedDate = ordIds[ot].CreatedDate,
                        UpdatedDate = ordIds[ot].UpdatedDate,
                        ProductId = prdIds[random.Next(0, prdIds.Count)].Id,
                        Description = "Deneme"

                    };
                    await service.OrderDetailRepository.AddAsync(orderDetail);
                }
                return Ok("Başarılı");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }



        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await service.OrderRepository.GetByIdAsync(id);

            if (result is not null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {
            var result = await service.OrderRepository.GetListAsync(default, false);

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

            var data = service.OrderDetailRepository.GetAll(x => x.Order.Active, true, false, x => x.Order);

            if (filter.ToLower() == "day")
                data = data.Where(x => x.CreatedDate > new DateTime(dt.Year, dt.Month, dt.Day));
            else if (filter.ToLower() == "month")
                data = data.Where(x => x.CreatedDate > new DateTime(dt.Year, dt.Month, 1));
            else if (filter.ToLower() == "year")
                data = data.Where(x => x.CreatedDate > new DateTime(dt.Year, 1, 1));

            var datas = await data.Where(x => x.Order.Employee.Active).GroupBy(x => x.Order.Employee.Fullname)
                                      .Select(x => new
                                      {
                                          EmployeeId = x.Key.ToString(),
                                          Count = x.Count()
                                      }).OrderByDescending(x => x.Count).ToListAsync();
            if (datas is not null)
            {
                return Ok(datas);
            }
            return BadRequest();
        }
    }
}
