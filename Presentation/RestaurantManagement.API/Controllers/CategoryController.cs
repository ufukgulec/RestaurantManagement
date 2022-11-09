using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            var data = await _categoryRepository.GetListAsync();
            return Ok(data);
        }
        [HttpGet("GetAlll")]
        public IActionResult GetList()
        {
            var data = _categoryRepository.GetList();
            return Ok(data);
        }
        [HttpGet("GetAll/{justActive}")]
        public async Task<IActionResult> GetAll(bool justActive)
        {
            var data = await _categoryRepository.GetListAsync(default, justActive);
            return Ok(data);
        }
    }
}
