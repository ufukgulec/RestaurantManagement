using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Repositories;
using RestaurantManagement.Application;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.MVC.Models.ViewModels;

namespace RestaurantManagement.MVC.Controllers
{
    public class OSController : BaseController
    {

        public OSController(IUnitOfWork service) : base(service)
        {

        }
        public IActionResult Index()
        {
            var data = service.OSAlertRepository.GetList();
            return View(data);
        }
        public async Task<IActionResult> List()
        {
            var data = await service.OSHeaderRepository.GetListAsync();
            return View(data);
        }
        public async Task<IActionResult> HeaderAction()
        {
            var data = await service.OSHeaderActionRepository.GetListAsync(default, false, x => x.OSHeader);
            ViewBag.Headers = await service.OSHeaderRepository.GetListAsync();
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> DXInsert([FromBody] Employee employee)
        {

            return Ok();


        }
        [HttpPost]
        public async Task<IActionResult> DXUpdate([FromBody] Employee employee)
        {

            return Ok();

        }
        [HttpPost]
        public async Task<IActionResult> DXRemove([FromBody] Employee employee)
        {

            return Ok(new Employee());


        }
    }
}
