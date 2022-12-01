using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application;
using RestaurantManagement.Application.Repositories;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.MVC.Controllers
{
    public class OrderTypeController : BaseController
    {
        private readonly IOrderTypeRepository _service;

        public OrderTypeController(IUnitOfWork service) : base(service)
        {
            _service = service.OrderTypeRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> List()
        {
            var data = await _service.GetListAsync(default, false);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> DXInsert([FromBody] OrderType entity)
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
        public async Task<IActionResult> DXUpdate([FromBody] OrderType entity)
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
        public async Task<IActionResult> DXRemove([FromBody] OrderType entity)
        {
            var result = await _service.Remove(entity);
            if (result)
            {
                return Ok(new OrderType());
            }
            else
            {
                return BadRequest(new OrderType());
            }

        }
    }
}
