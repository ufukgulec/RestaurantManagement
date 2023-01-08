using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestaurantManagement.Application;
using RestaurantManagement.Application.Repositories;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.MVC.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductRepository _service;
        public ProductController(IUnitOfWork service) : base(service)
        {
            _service = service.ProductRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> List()
        {
            ViewBag.PageHeader = "Ürünler Tablosu";
            ViewBag.Categories = await service.CategoryRepository.GetListAsync(default, false);
            var data = await _service.GetListAsync(default, false);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> DXInsert([FromBody] Product entity)
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
        public async Task<IActionResult> DXUpdate([FromBody] Product entity)
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
        public async Task<IActionResult> DXRemove([FromBody] Product entity)
        {
            var result = await _service.Remove(entity);
            if (result)
            {
                return Ok(new Product());
            }
            else
            {
                return BadRequest(new Product());
            }

        }

        public IActionResult Chart()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetProductRelatedInformation()
        {
            var data = _service.GetAll(default, false, false).OrderByDescending(x => x.Price).Select(r => new
            {
                arg = r.Name,
                val = r.Price
            }).ToList();

            return Json(JsonConvert.SerializeObject(data));
        }
    }
}
