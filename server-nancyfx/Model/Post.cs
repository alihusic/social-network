using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork2.Model
{

    /// <summary>
    /// Class used as database model for Post table.
    /// Class created by Ermin.
    /// </summary>

    public class Post
    {
        [Key]
        public int postId { get; set; }
        public string postContent { get; set; }
        public DateTime postCreationTimeStamp { get; set; }
        public int creatorId { get; set; }
        public int targetId { get; set; }
        public int numOfLikes { get; set; }

        //setting up 1:n relation with User
        public virtual User user { get; set; }

        //setting up 1:n relation with Likes
        [JsonIgnore]
        public virtual ICollection<Like> likes { get; set; }

        //setting up 1:n relation with comments
        [JsonIgnore]
        public virtual ICollection<Comment> comments { get; set; }
    }
}
