using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Model
{

    //model for Posts table
    //used for storage of posts 

    public class Posts
    {
        [Key]
        public int postsId { get; set; }
        public string postContent { get; set; }
        public DateTime postCreationDate { get; set; }
        public int creatorId { get; set; }
        public int targetId { get; set; }
        public int numOfLikes { get; set; }

        //creating 1:n connection with user
        public virtual User user { get; set; }

        //creating 1:n connection with likes
        public virtual ICollection<Likes> likes { get; set; }

        //setting up 1:n relation with comments
        public virtual ICollection<Comments> comments { get; set; }
    }
}
