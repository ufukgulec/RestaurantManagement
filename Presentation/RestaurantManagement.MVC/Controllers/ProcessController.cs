using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application;
using RestaurantManagement.Application.Repositories;
using RestaurantManagement.Domain.Entities;
using System.Text.Json;

namespace RestaurantManagement.MVC.Controllers
{
    public class ProcessController : BaseController
    {
        private readonly IProcessRepository _service;
        public ProcessController(IUnitOfWork service) : base(service)
        {
            _service = service.ProcessRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> List()
        {
            ViewBag.PageHeader = "Süreçler Tablosu";
            var data = await _service.GetListAsync(default, false);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> DXInsert([FromBody] Process entity)
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
        public async Task<IActionResult> DXUpdate([FromBody] Process entity)
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
        public async Task<IActionResult> DXRemove([FromBody] Process entity)
        {
            var result = await _service.Remove(entity);
            if (result)
            {
                return Ok(new Process());
            }
            else
            {
                return BadRequest(new Process());
            }

        }

    }
}
