using Microsoft.AspNetCore.Mvc;

namespace RestaurantManagement.MVC.ViewComponents
{
    public class Menu : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
