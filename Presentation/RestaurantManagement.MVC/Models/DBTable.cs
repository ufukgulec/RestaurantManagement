namespace RestaurantManagement.MVC.Models
{
    public class DBTable
    {
        private readonly ICollection<string> _columns = new List<string>();

        public string Name { get; set; }
        public ICollection<string>? items
        {
            get
            {
                return _columns;
            }
            set
            {

            }
        }
    }
}
