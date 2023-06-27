namespace RestaurantManagement.Shared.DTO
{
    public class RoleDTO : BaseDTO
    {
        public string Name { get; set; }

        public ICollection<EmployeeDTO>? Employees { get; set; }
    }
}
