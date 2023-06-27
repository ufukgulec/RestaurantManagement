namespace RestaurantManagement.Shared.DTO
{
    public class BaseDTO
    {
        public Guid Id { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
