namespace RestaurantManagement.Shared.DTO
{
    public class NotificationTypeDTO : BaseDTO
    {
        public string Name { get; set; }
        public string Class { get; set; }
        public string Icon { get; set; }
        public ICollection<NotificationDTO>? Notifications { get; set; }
    }
}
