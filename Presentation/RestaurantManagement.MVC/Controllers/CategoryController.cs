using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NuGet.Protocol;
using RestaurantManagement.Application;
using RestaurantManagement.Application.Repositories;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.MVC.Models.ViewModels;
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
        public async Task<IActionResult> Index()
        {
            ViewBag.PageHeader = "Kategori Paneli";

            VMCategoryModel categoryModel = new VMCategoryModel(service);

            return View(categoryModel);
        }
        public async Task<IActionResult> List()
        {
            ViewBag.PageHeader = "Kategori Tablosu";
            var data = await _service.GetListAsync(default, false);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> DXInsert([FromBody] Category category)
        {
            try
            {
                var exist = await _service.GetSingleAsync(x => x.Name.ToLower() == category.Name.ToLower());

                if (exist is null)
                {
                    var result = await _service.AddAsync(category);

                    if (result)
                    {
                        return Ok(category);
                    }
                    else
                    {
                        return BadRequest("Ekleme işleminde bir hata oluştu!");
                    }

                }
                else
                {
                    return BadRequest("Aynı isimde kategori olamaz!");
                }
            }
            catch (Exception)
            {
                return BadRequest("Herhangi bir hata oluştu!");
            }


        }
        [HttpPost]
        public async Task<IActionResult> DXUpdate([FromBody] Category category)
        {
            try
            {
                var exist = await _service.GetSingleAsync(x => x.Name.ToLower() == category.Name.ToLower() && x.Id != category.Id);

                if (exist is null)
                {
                    var result = await _service.Update(category);
                    if (result)
                    {
                        return Ok(category);
                    }
                    else
                    {
                        return BadRequest("Ekleme işleminde bir hata oluştu!");
                    }
                }
                else
                {
                    return BadRequest("Aynı isimde kategori olamaz!");

                }
            }
            catch (Exception)
            {
                return BadRequest("Herhangi bir hata oluştu!");
            }

        }
        [HttpPost]
        public async Task<IActionResult> DXRemove([FromBody] Category category)
        {
            try
            {
                var cat = await _service.GetListAsync(x => x.Id == category.Id, false, x => x.Products);
                if (cat.First().Products.Count == 0)
                {
                    var result = await _service.Remove(category);
                    if (result)
                    {
                        return Ok(category);
                    }
                    else
                    {
                        return BadRequest("Silme işleminde hata oldu!");

                    }
                }
                else
                {
                    return BadRequest("İlişkili ürünler varken silme yapılamaz!");

                }
            }
            catch (Exception)
            {
                return BadRequest("Herhangi bir hata oluştu!");
            }


        }
        [HttpGet]
        public async Task<IActionResult> GetData()
        {
            var data = await _service.GetAll(default, true, false)
                .Select(c => new
                {
                    Id = c.Id,
                    Status = c.Active,
                    Name = c.Name,
                    Description = c.Description,
                    UpdatedDate = c.UpdatedDate
                }).ToListAsync();
            var json = Json(JsonConvert.SerializeObject(data));
            return json;
        }

        public IActionResult Chart()
        {
            ViewBag.PageHeader = "Kategori Grafikleri";
            return View();
        }
        [HttpGet]
        public IActionResult GetProductRelatedInformation()
        {
            var data = _service.GetAll(default, false, false, x => x.Products).Select(r => new
            {
                arg = r.Name,
                val = r.Products.Count
            }).ToList();

            return Json(JsonConvert.SerializeObject(data));
        }
        [HttpGet]
        public async Task<IActionResult> GetOrderRelatedInformation(string filter)
        {
            IOrderDetailRepository.Filter f = (IOrderDetailRepository.Filter)Enum.Parse(typeof(IOrderDetailRepository.Filter), filter);
            var data = await service.OrderDetailRepository.GetCategoryRelatedInformation(f);

            return Json(JsonConvert.SerializeObject(data));
        }

    }
}
