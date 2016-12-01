using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Model
{

    //model for PendingFriendRequest table
    //will be used to create and regulate friendsip requests in database


    public class PendingFriendRequests
    {
        [Key]
        public int pendingFriendRequestId { get; set; }
        public int senderId { get; set; }
        public int receiverId { get; set; }
        public DateTime friendRequestSent { get; set; }
        public bool friendRequestConfirmed { get; set; }

        
        
        public virtual User user { get; set; }

    }
}
