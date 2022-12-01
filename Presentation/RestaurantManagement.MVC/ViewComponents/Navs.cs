using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Application;

namespace RestaurantManagement.MVC.ViewComponents.Navs
{
    public class Navs : ViewComponent
    {
        private readonly IUnitOfWork service;

        public Navs(IUnitOfWork service)
        {
            this.service = service;
        }
        public IViewComponentResult Invoke()
        {
            var data = service.SectionRepository
                .GetNavs();
            return View(data);
        }
    }
}
