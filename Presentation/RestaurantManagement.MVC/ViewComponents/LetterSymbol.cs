using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.MVC.Models.ComponentModels;
using System.Collections.Generic;

namespace RestaurantManagement.MVC.ViewComponents
{
    public class LetterSymbol : ViewComponent
    {
        public IViewComponentResult Invoke(WidgetModel model)
        {
            return View(model);
        }
    }
}
