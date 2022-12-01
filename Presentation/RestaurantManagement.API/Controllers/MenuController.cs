using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application;

namespace RestaurantManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : BaseController
    {
        public MenuController(IUnitOfWork service) : base(service)
        {

        }
        [HttpGet]
        public async Task<IActionResult> Metot()
        {
            //await service.SectionRepository.AddAsync(new Domain.AppEntities.MenuSection()
            //{
            //    Caption = "Ayarlar",
            //    RowNumber = 0,
            //});
            //await service.TopMenuRepository.AddAsync(new Domain.AppEntities.TopMenu()
            //{
            //    Caption = "Menü",
            //    Icon = "Deneme",
            //    RowNumber = 0,
            //    MenuSectionId = Guid.Parse("84EEFF55-6A9B-418C-972A-08DACAEED267"),
            //    Active = true,
            //});
            await service.SubMenuRepository.AddAsync(new Domain.AppEntities.SubMenu()
            {
                Caption = "Sıralama",
                ClickCounter = 0,
                Link = "/Menu/Order",
                RowNumber = 0,
                TopMenuId = Guid.Parse("24A26B6D-B8C7-4DDF-F37E-08DACAEF422F"),
                Active = true
            });
            return Ok("Başarılı");
        }
    }
}
