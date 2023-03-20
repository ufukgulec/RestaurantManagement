using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using RestaurantManagement.Application;
using RestaurantManagement.Application.Repositories;
using RestaurantManagement.Domain.Entities;
using System.Linq.Expressions;

namespace RestaurantManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController, IController<Category>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(IUnitOfWork service) : base(service)
        {
            _categoryRepository = service.CategoryRepository;
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _categoryRepository.GetByIdAsync(id);

            if (result is not null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {
            var result = await _categoryRepository.GetListAsync(default,false);

            if (result is not null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet("GetOrderList/{columnName}/{orderType}")]
        public async Task<IActionResult> GetOrderListAsync(string columnName, string orderType)
        {
            var result = await _categoryRepository.GetListAsync(default, false);


            if (result is not null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
