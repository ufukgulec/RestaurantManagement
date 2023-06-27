namespace RestaurantManagement.Shared.DTO
{
    public class OrderDetailDTO : BaseDTO
    {//Order-Product ara tablosu
        public string Description { get; set; }

        public Guid OrderId { get; set; }
        public OrderDTO? Order { get; set; }
        public Guid ProductId { get; set; }
        public ProductDTO? Product { get; set; }
    }
}
