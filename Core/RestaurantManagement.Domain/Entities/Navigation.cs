namespace RestaurantManagement.Domain.Entities
{
    public class Navigation : BaseEntity
    {
        public string ParentId { get; set; }
        public string Caption { get; set; }
        public string? Href { get; set; }
        public string? Icon { get; set; }
        public string Tags { get; set; }
    }
}
