using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Repositories;
using RestaurantManagement.Application;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.MVC.Controllers
{
    public class IngredientController : BaseController
    {
        private readonly IIngredientRepository _service;

        public IngredientController(IUnitOfWork service) : base(service)
        {
            _service = service.IngredientRepository;
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
        public async Task<IActionResult> DXInsert([FromBody] Ingredient ingredient)
        {
            var result = await _service.AddAsync(ingredient);
            if (result)
            {
                return Ok(ingredient);
            }
            else
            {
                return BadRequest(ingredient);
            }

        }
        [HttpPost]
        public async Task<IActionResult> DXUpdate([FromBody] Ingredient ingredient)
        {
            var result = await _service.Update(ingredient);
            if (result)
            {
                return Ok(ingredient);
            }
            else
            {
                return BadRequest(ingredient);
            }

        }
        [HttpPost]
        public async Task<IActionResult> DXRemove([FromBody] Ingredient ingredient)
        {
            var result = await _service.Remove(ingredient);
            if (result)
            {
                return Ok(new Ingredient());
            }
            else
            {
                return BadRequest(new Ingredient());
            }

        }
    }
}
