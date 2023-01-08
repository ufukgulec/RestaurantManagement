using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.MVC.Models.ComponentModels;

namespace RestaurantManagement.MVC.ViewComponents
{
    public class Search:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
