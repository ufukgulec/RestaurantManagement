namespace RestaurantManagement.Shared.DTO
{
    public class OrderDTO : BaseDTO
    {
        public string? Name { get; set; }
        public Guid OrderTypeId { get; set; }
        public OrderTypeDTO? OrderType { get; set; }

        public Guid EmployeeId { get; set; }
        public EmployeeDTO? Employee { get; set; }

        public Guid ProcessId { get; set; }
        public ProcessDTO? Process { get; set; }

        public ICollection<OrderDetailDTO>? OrderDetails { get; set; }
    }
}
