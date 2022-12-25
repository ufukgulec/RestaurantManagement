using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.MVC.Models.ViewModels
{
    public class VMBaseModel<T> where T : BaseEntity
    {
        protected readonly List<T> _list;

        public VMBaseModel(List<T> list)
        {
            _list = list;
        }
        public int getActiveCount
        {
            get { return _list.Where(x => x.Active).Count(); }
        }
        public int getPassiveCount
        {
            get { return _list.Where(x => x.Active == false).Count(); }
        }
    }
}
