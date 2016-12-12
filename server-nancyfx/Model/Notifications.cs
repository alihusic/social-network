<<<<<<< HEAD
﻿using System.ComponentModel.DataAnnotations;
using SocialNetwork.Model;

namespace SocialNetworkServer.Model
{
    public class Notifications
    {
        [Key]
        public int notificationsId { get; set; }
        public int notificationType { get; set; }
        public int creatorId { get; set; }
        public int entityTargetId { get; set; }


        //setting up 1:n relation with user
        public virtual User user { get; set; }


    }
}
=======
﻿using System.ComponentModel.DataAnnotations;
using SocialNetwork.Model;

namespace SocialNetworkServer.Model
{
    public class Notifications
    {
        [Key]
        public int notificationsId { get; set; }
        public int notificationType { get; set; }
        public int creatorId { get; set; }
        public int entityTargetId { get; set; }


        //setting up 1:n relation with user
        public virtual User user { get; set; }


    }
}
>>>>>>> refs/remotes/origin/Maulwurf
