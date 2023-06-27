namespace RestaurantManagement.Shared.DTO
{
    public class OrderTypeDTO : BaseDTO
    {
        public string Name { get; set; }
        public ICollection<OrderDTO>? Orders { get; set; }
    }
}
