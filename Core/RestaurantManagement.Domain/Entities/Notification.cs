using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Domain.Entities
{
    public class Notification : BaseEntity
    {
        public string Caption { get; set; }
        public string Text { get; set; }
        public int Type { get; set; }

        public Guid NotificationTypeId { get; set; }
        public NotificationType? NotificationType { get; set; }

    }
}
