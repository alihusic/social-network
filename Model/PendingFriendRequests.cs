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
        public int userId { get; set; }
        public int user2 { get; set; }
        public DateTime friendRequestSent { get; set; }
        public bool firendRequestConfirmed { get; set; }

        
        
        public virtual User user { get; set; }

    }
}
