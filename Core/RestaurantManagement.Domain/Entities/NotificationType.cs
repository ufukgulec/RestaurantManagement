using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Domain.Entities
{
    public class NotificationType : BaseEntity
    {
        public string Name { get; set; }
        public string Class { get; set; }
        public string Icon { get; set; }
        public ICollection<Notification>? Notifications { get; set; }
    }
}
