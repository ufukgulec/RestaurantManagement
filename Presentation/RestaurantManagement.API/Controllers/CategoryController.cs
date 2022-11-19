using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using RestaurantManagement.Application;
using RestaurantManagement.Application.Repositories;

namespace RestaurantManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(IUnitOfWork service) : base(service)
        {
            _categoryRepository = service.CategoryRepository;
        }
        [HttpGet("AdvancedGetAll")]
        [EnableQuery]
        public IActionResult advancedGetAll()
        {
            var data = _categoryRepository.GetAll(default, false);
            return Ok(data);
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var data = await _categoryRepository.GetByIdAsync(id);
            return Ok(data);
        }
    }
}
