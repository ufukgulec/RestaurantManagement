using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Repositories;
using RestaurantManagement.Application;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.MVC.Controllers
{
    public class RoleController : BaseController
    {
        private readonly IRoleRepository _service;

        public RoleController(IUnitOfWork service) : base(service)
        {
            _service = service.RoleRepository;
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
        public async Task<IActionResult> DXInsert([FromBody] Role entity)
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
        public async Task<IActionResult> DXUpdate([FromBody] Role entity)
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
        public async Task<IActionResult> DXRemove([FromBody] Role entity)
        {
            var result = await _service.Remove(entity);
            if (result)
            {
                return Ok(new Role());
            }
            else
            {
                return BadRequest(new Role());
            }

        }
    }
}
