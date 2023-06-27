namespace RestaurantManagement.Shared.DTO
{
    public class SupplierDTO : BaseDTO
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public ICollection<WholesaleDTO>? Wholesales { get; set; }
    }
}
