using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestClientSN.Model
{
    class Notifications
    {
        public int notificationsId { get; set; }
        public int notificationType { get; set; }
        public int creatorId { get; set; }
        public int entityTargetId { get; set; }
    }
}
