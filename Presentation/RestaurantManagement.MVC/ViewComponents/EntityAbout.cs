using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.MVC.Models.ComponentModels;

namespace RestaurantManagement.MVC.ViewComponents
{
    public class EntityAbout : ViewComponent
    {
        public IViewComponentResult Invoke(WidgetModel model)
        {
            return View(model);
        }
    }
}
