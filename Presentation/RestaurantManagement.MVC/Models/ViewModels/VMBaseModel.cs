using RestaurantManagement.Application;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.MVC.Models.ViewModels
{
    public class VMBaseModel
    {
        protected readonly IUnitOfWork service;

        public VMBaseModel(IUnitOfWork service)
        {
            this.service = service;
        }
    }
}
