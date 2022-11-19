using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application;

namespace RestaurantManagement.MVC.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IUnitOfWork service;

        public BaseController(IUnitOfWork service)
        {
            this.service = service;
        }
    }
}
