using System;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork2.Model
{

    /// <summary>
    /// Class used as database model for PendingFriendRequest table.
    /// Class created by Ermin.
    /// </summary>

    public class PendingFriendRequest
    {
        [Key]
        public int pendingFriendRequestId { get; set; }
        public int senderId { get; set; }
        public int receiverId { get; set; }
        public DateTime friendRequestTimeStamp { get; set; }
        public bool friendRequestConfirmed { get; set; }


        //setting up 1:n relation with User
        public virtual User user { get; set; }

    }
}
