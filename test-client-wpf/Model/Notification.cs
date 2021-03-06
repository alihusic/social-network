﻿using SocialNetwork2.Model;

namespace SocialNetworkServer.Model
{
    public class Notification
    {
        public int notificationId { get; set; }
        public int notificationType { get; set; }
        public int creatorId { get; set; }
        public int entityTargetId { get; set; }


        //setting up 1:n relation with User
        public virtual User user { get; set; }


    }
}
