using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
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
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(IUnitOfWork service) : base(service)
        {
            _categoryRepository = service.CategoryRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> List()
        {
            var data = await _categoryRepository.GetListAsync(default, false);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> DXInsert([FromBody] Category category)
        {
            var result = await _categoryRepository.AddAsync(category);
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
            var result = await _categoryRepository.Update(category);
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
        public async Task<IActionResult> DXRemove([FromBody] string Id)
        {
            var result = await _categoryRepository.Remove(Id);
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
        [EnableQuery]
        public IActionResult GetJsonData()
        {
            var settings = new JsonSerializerOptions
            {
                PropertyNamingPolicy = null,
            };

            var data = _categoryRepository.GetAll(default, false);

            return Json(data, settings);
        }
    }
}
