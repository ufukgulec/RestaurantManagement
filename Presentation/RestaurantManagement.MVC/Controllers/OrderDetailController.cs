using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application;
using RestaurantManagement.Application.Repositories;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.MVC.Controllers
{
    public class OrderDetailController : BaseController
    {
        private readonly IOrderDetailRepository _service;

        public OrderDetailController(IUnitOfWork service) : base(service)
        {
            _service = service.OrderDetailRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> List()
        {
            var data = await _service.GetListAsync(default, false);
            ViewBag.Orders = await service.OrderRepository.GetListAsync(default, false);
            ViewBag.Products = await service.ProductRepository.GetListAsync(default, false);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> DXInsert([FromBody] OrderDetail entity)
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
        public async Task<IActionResult> DXUpdate([FromBody] OrderDetail entity)
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
        public async Task<IActionResult> DXRemove([FromBody] OrderDetail entity)
        {
            var result = await _service.Remove(entity);
            if (result)
            {
                return Ok(new OrderDetail());
            }
            else
            {
                return BadRequest(new OrderDetail());
            }

        }
    }
}
