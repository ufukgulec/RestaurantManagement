using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.MVC.Models.ComponentModels;

namespace RestaurantManagement.MVC.ViewComponents
{
    public class MostOrdered : ViewComponent
    {
        public IViewComponentResult Invoke(WidgetModel model)
        {
            return View(model);
        }
    }
}
