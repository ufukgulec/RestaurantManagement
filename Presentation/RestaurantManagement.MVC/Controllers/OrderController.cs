using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Repositories;
using RestaurantManagement.Application;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.MVC.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderRepository _service;

        public OrderController(IUnitOfWork service) : base(service)
        {
            _service = service.OrderRepository;
        }

        public IActionResult Index()
        {
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
