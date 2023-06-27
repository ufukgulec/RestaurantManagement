namespace RestaurantManagement.Shared.DTO
{
    public class EmployeeDTO : BaseDTO
    {
        public string Fullname { get; set; }
        public string PhoneNumber { get; set; }

        public Guid RoleId { get; set; }
        public RoleDTO? Role { get; set; }

        public ICollection<OrderDTO>? Orders { get; set; }

    }
}
