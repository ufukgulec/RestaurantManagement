namespace RestaurantManagement.Shared.DTO
{
    public class ProcessDTO : BaseDTO
    {
        public string Name { get; set; }
        public string IconUrl { get; set; }

        public ICollection<OrderDTO>? Orders { get; set; }
    }
}
