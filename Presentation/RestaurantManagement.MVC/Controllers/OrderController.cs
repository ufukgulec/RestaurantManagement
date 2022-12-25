using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Repositories;
using RestaurantManagement.Application;
using RestaurantManagement.Domain.Entities;
using Bogus.DataSets;

namespace RestaurantManagement.MVC.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderRepository _service;

        public OrderController(IUnitOfWork service) : base(service)
        {
            _service = service.OrderRepository;
        }

        public async Task<IActionResult> Index()
        {
            var OrderTypeIds = await service.OrderTypeRepository.GetListAsync();
            var empIds = await service.EmployeeRepository.GetListAsync();
            var prcIds = await service.ProcessRepository.GetListAsync();

            Random random = new Random();
            for (int i = 0; i < 50; i++)
            {
                int ot = random.Next(0, OrderTypeIds.Count);

                Order order = new Order
                {
                    Active = true,
                    CreatedDate = new DateTime(random.Next(2020, DateTime.Now.Year), random.Next(1, 12), random.Next(1, 30)),
                    UpdatedDate = DateTime.Now,
                    EmployeeId = empIds[random.Next(0, empIds.Count)].Id,
                    OrderTypeId = OrderTypeIds[ot].Id,
                    ProcessId = prcIds[random.Next(0, prcIds.Count)].Id,
                    Name = DateTime.Now.ToString("ddMM") + "-SPR" + OrderTypeIds[ot].Name[0].ToString().ToUpper() + "-" + i.ToString().PadLeft(4, '0'),
                };
                await _service.AddAsync(order);

            }
            var ordIds = await service.OrderRepository.GetListAsync();
            var prdIds = await service.ProductRepository.GetListAsync();

            for (int i = 0; i < 100; i++)
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

            return View();
        }
        public async Task<IActionResult> List()
        {
            var data = await _service.GetListAsync(default, false, x => x.OrderType, x => x.Employee, x => x.Process);

            ViewBag.Types = await service.OrderTypeRepository.GetListAsync(default, false);
            ViewBag.Employee = await service.EmployeeRepository.GetListAsync(default, false);
            ViewBag.Process = await service.ProcessRepository.GetListAsync(default, false);

            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> DXInsert([FromBody] Order entity)
        {
            var data = await _service.GetListAsync(x => x.OrderTypeId == entity.OrderTypeId);
            var orderType = await service.OrderTypeRepository.GetByIdAsync(entity.OrderTypeId.ToString());
            entity.Name = DateTime.Now.ToString("ddMM") + "-SPR" + orderType.Name[0].ToString().ToUpper() + "-" + data.Count.ToString().PadLeft(4, '0');
            var result = await _service.AddAsync(entity);
            if (result)
            {
                return Ok(entity);
            }
            else
            {
                return BadRequest(entity);
            }

        }
        [HttpPost]
        public async Task<IActionResult> DXUpdate([FromBody] Order entity)
        {
            var result = await _service.Update(entity);
            if (result)
            {

                return Ok(entity);
            }
            else
            {
                return BadRequest(entity);
            }

        }
        [HttpPost]
        public async Task<IActionResult> DXRemove([FromBody] Order entity)
        {
            var result = await _service.Remove(entity);
            if (result)
            {
                return Ok(new Order());
            }
            else
            {
                return BadRequest(new Order());
            }

        }
    }
}
