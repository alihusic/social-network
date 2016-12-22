using SocialNetworkServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkServer.Builder
{
    class NotificationBuilder
    {
        private int notificationType;
        private int creatorId;
        private int entityTargetId;


        public NotificationBuilder NotificationType(int notificationType)
        {
            this.notificationType = notificationType;
            return this;
        }

        public NotificationBuilder CreatorId(int creatorId)
        {
            this.creatorId = creatorId;
            return this;
        }

        public NotificationBuilder EntityTargetId(int entityTargetId)
        {
            this.entityTargetId = entityTargetId;
            return this;
        }


        public Notification Build()
        {
            return new Notification()
            {
                notificationType = notificationType,
                creatorId = creatorId,
                entityTargetId = entityTargetId
            };
        }
    }
}
