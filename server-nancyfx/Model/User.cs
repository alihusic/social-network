using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace SocialNetwork2.Model
{

    /// <summary>
    /// Class used as database model for User table.
    /// Class created by Ermin.
    /// </summary>

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
        public virtual ICollection<PrivateMessage> privateMessages { get; set; }

        //setting up 1:n relation with Posts
        [JsonIgnore]
        public virtual ICollection<Post> posts { get; set; }

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

        //setting up 1:n realtion with Notifications
        [JsonIgnore]
        public virtual ICollection<Notification> notifications { get; set; }

        //setting up 1:n relation with unreadMessages
        [JsonIgnore]
        public virtual ICollection<UnreadMessage> unreadMessages { get; set; }
    }
}
