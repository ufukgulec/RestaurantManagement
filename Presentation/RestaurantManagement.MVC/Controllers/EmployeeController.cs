using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Repositories;
using RestaurantManagement.Application;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.MVC.Controllers
{
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeRepository _service;

        public EmployeeController(IUnitOfWork service) : base(service)
        {
            _service = service.EmployeeRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> List(string id)
        {
            ViewBag.PageHeader = "Çalışanlar Tablosu";
            List<Employee> data;
            if (id is null)
            {
                data = await _service.GetListAsync(default, false, x => x.Role);
            }
            else
            {
                data = await _service.GetListAsync(x => x.RoleId.Equals(Guid.Parse(id)), false, x => x.Role);
            }
            ViewBag.Roles = await service.RoleRepository.GetListAsync(default, false);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> DXInsert([FromBody] Employee employee)
        {
            var result = await _service.AddAsync(employee);
            if (result)
            {
                return Ok(employee);
            }
            else
            {
                return BadRequest(employee);
            }

        }
        [HttpPost]
        public async Task<IActionResult> DXUpdate([FromBody] Employee employee)
        {
            var result = await _service.Update(employee);
            if (result)
            {
                return Ok(employee);
            }
            else
            {
                return BadRequest(employee);
            }

        }
        [HttpPost]
        public async Task<IActionResult> DXRemove([FromBody] Employee employee)
        {
            var result = await _service.Remove(employee);
            if (result)
            {
                return Ok(new Employee());
            }
            else
            {
                return BadRequest(new Employee());
            }

        }
    }
}
