using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application;
using RestaurantManagement.Domain.AppEntities;

namespace RestaurantManagement.MVC.Controllers
{
    public class MenuData
    {
        public string Caption { get; set; }
        public string? Id { get; set; }
        public string? ParentId { get; set; }
        public int? RowNumber { get; set; }
        public string? Link { get; set; }
        public bool? isSection { get; set; }
        public bool? isTopMenu { get; set; }
    }
    public class MenuController : BaseController
    {
        public MenuController(IUnitOfWork service) : base(service)
        {
        }

        public IActionResult Index()
        {
            var sections = service.SectionRepository.GetList().Select(x => new MenuData
            {
                Id = x.Id.ToString(),
                Caption = x.Caption,
                ParentId = "0",
                RowNumber = x.RowNumber,
                isSection = true,
                isTopMenu = false,
            }).ToList();
            var topmenus = service.TopMenuRepository.GetList().Select(x => new MenuData
            {
                Id = x.Id.ToString(),
                Caption = x.Caption,
                ParentId = x.MenuSectionId.ToString(),
                RowNumber = x.RowNumber,
                isSection = false,
                isTopMenu = true
            }).ToList();
            var submenus = service.SubMenuRepository.GetList(default, default, x => x.TopMenu).Select(x => new MenuData
            {
                Id = x.Id.ToString(),
                Caption = x.Caption,
                ParentId = x.TopMenuId.ToString(),
                RowNumber = x.RowNumber,
                Link = x.Link,
                isSection = false,
                isTopMenu = false,
            }).ToList();

            sections.AddRange(topmenus);
            sections.AddRange(submenus);

            return View(sections);
        }
        [HttpPost]
        public IActionResult DXInsert([FromBody] MenuData? menuData)
        {
            return View(menuData);
        }
    }
}
