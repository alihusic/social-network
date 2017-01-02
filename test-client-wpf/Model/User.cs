using System;
using System.Collections.Generic;
using SocialNetworkServer.Model;
using Newtonsoft.Json;

namespace SocialNetwork2.Model
{

    //model for User table
    //used for sotrage of users information 

    public class User
    {
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

        //setting up 1:n relation with PrivateMessage
        [JsonIgnore]
        public virtual ICollection<PrivateMessage> PrivateMessage { get; set; }

        //setting up 1:n relation with Post
        [JsonIgnore]
        public virtual ICollection<Post> Post { get; set; }

        //setting up 1:n connection with PendingFriendRequest
        [JsonIgnore]
        public virtual ICollection<PendingFriendRequest> pendingFriendRequests { get; set; }

        //setting up 1:n connection with Likes
        [JsonIgnore]
        public virtual ICollection<Like> likes { get; set; }

        //setting up 1:1 connection with Token
        [JsonIgnore]
        public virtual Token token { get; set; }

        //setting up 1:n connection with Comments
        [JsonIgnore]
        public virtual ICollection<Comment> comments { get; set; }

        //setting up 1:n realtion with Notification
        [JsonIgnore]
        public virtual ICollection<Notification> Notification { get; set; }

        //setting up 1:n relation with unreadMessages
        [JsonIgnore]
        public virtual ICollection<UnreadMessage> unreadMessages { get; set; }
    }
}
