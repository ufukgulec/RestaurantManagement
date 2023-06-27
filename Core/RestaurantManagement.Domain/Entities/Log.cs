namespace RestaurantManagement.Domain.Entities
{
    public enum LogType
    {
        Information,
        Warning,
        Error
    }
    public class Log : BaseEntity
    {
        public string? Message { get; set; }
        public LogType LogType { get; set; }
    }
}
