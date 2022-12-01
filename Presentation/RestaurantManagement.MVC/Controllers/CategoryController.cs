using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NuGet.Protocol;
using RestaurantManagement.Application;
using RestaurantManagement.Application.Repositories;
using RestaurantManagement.Domain.Entities;
using System.Text;
using System.Text.Json;

namespace RestaurantManagement.MVC.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryRepository _service;

        public CategoryController(IUnitOfWork service) : base(service)
        {
            _service = service.CategoryRepository;
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
        public async Task<IActionResult> DXInsert([FromBody] Category category)
        {
            var result = await _service.AddAsync(category);
            if (result)
            {
                return Ok(category);
            }
            else
            {
                return BadRequest(category);
            }

        }
        [HttpPost]
        public async Task<IActionResult> DXUpdate([FromBody] Category category)
        {
            var result = await _service.Update(category);
            if (result)
            {
                return Ok(category);
            }
            else
            {
                return BadRequest(category);
            }

        }
        [HttpPost]
        public async Task<IActionResult> DXRemove([FromBody] Category category)
        {
            var result = await _service.Remove(category);
            if (result)
            {
                return Ok(new Category());
            }
            else
            {
                return BadRequest(new Category());
            }

        }
        [HttpGet]
        public IActionResult GetJsonData()
        {
            var settings = new JsonSerializerOptions
            {
                PropertyNamingPolicy = null,
            };

            var data = _service.GetAll(default, false);

            return Json(data, settings);
        }
    }
}
