using System;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork2.Model
{

    //model for PendingFriendRequest table
    //will be used to create and regulate friendsip requests in database


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
