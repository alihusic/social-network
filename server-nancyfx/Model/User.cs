using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SocialNetworkServer.Model;
using Newtonsoft.Json;

namespace SocialNetwork.Model
{

    //model for User table
    //used for sotrage of users information 

    public class User
    {
        [Key]
        public int userId { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string pictureURL { get; set; }
        public string coverPictureURL { get; set; }
        public string gender { get; set; }
        public DateTime dateOfBirth { get; set; }


        //setting up 1:n relation with PrivateChats
        [JsonIgnore]
        public virtual ICollection<PrivateChat> privateChats { get; set; }

        //setting up 1:n relation with PrivateMessages
        [JsonIgnore]
        public virtual ICollection<PrivateMessages> privateMessages { get; set; }

        //setting up 1:n connection with posts
        [JsonIgnore]
        public virtual ICollection<Posts> posts { get; set; }

        //setting up 1:n connection with PendingFriendRequest
        [JsonIgnore]
        public virtual ICollection<PendingFriendRequests> pendingFriendRequests { get; set; }

        //setting up 1:n connection with Likes
        [JsonIgnore]
        public virtual ICollection<Likes> likes { get; set; }

        //setting up 1:1 connection with Token
        [JsonIgnore]
        public virtual Token token { get; set; }

        //setting up 1:n connection with Comments
        [JsonIgnore]
        public virtual ICollection<Comments> comments { get; set; }

        //setting up 1:n realtion with Notifications
        [JsonIgnore]
        public virtual ICollection<Notifications> notifications { get; set; }

        //setting up 1:n relation with unreadMessages
        [JsonIgnore]
        public virtual ICollection<UnreadMessages> unreadMessages { get; set; }
    }
}
