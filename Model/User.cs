using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Model
{

    //model for User table
    //used for sotrage of users information 

    public class User
    {
        [Key]
        [Column(Order= 1)]
        public int userId { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string region { get; set; }
        public string gender { get; set; }
        public DateTime dateOfBirth { get; set; }


        //setting up 1:n relation with PrivateChats
        public virtual ICollection<PrivateChat> privateChats { get; set; }

        //setting up 1:n relation with PrivateMessages
        public virtual ICollection<PrivateMessages> privateMessages { get; set; }

        //setting up 1:n connection with posts
        public virtual ICollection<Posts> posts { get; set; }

        //setting up 1:n connection with PendingFriendRequest
        public virtual ICollection<PendingFriendRequests> pendingFriendRequests { get; set; }
       
        //setting up 1:n connection with Likes
        public virtual ICollection<Likes> likes { get; set; }

        //setting up 1:1 connection with Token
        public virtual Token token { get; set; }
    }
}
