namespace RestaurantManagement.Shared.DTO
{
    public class NotificationDTO : BaseDTO
    {
        public string Caption { get; set; }
        public string Text { get; set; }
        public int Type { get; set; }

        public Guid NotificationTypeId { get; set; }
        public NotificationTypeDTO? NotificationType { get; set; }

    }
}
