using SocialNetworkServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkServer.Builder
{
    class NotificationsBuilder
    {
        private int notificationType;
        private int creatorId;
        private int entityTargetId;


        public NotificationsBuilder NotificationType(int notificationType)
        {
            this.notificationType = notificationType;
            return this;
        }

        public NotificationsBuilder CreatorId(int creatorId)
        {
            this.creatorId = creatorId;
            return this;
        }

        public NotificationsBuilder EntityTargetId(int entityTargetId)
        {
            this.entityTargetId = entityTargetId;
            return this;
        }


        public Notifications Build()
        {
            return new Notifications()
            {
                notificationType = notificationType,
                creatorId = creatorId,
                entityTargetId = entityTargetId
            };
        }
    }
}
