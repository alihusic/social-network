﻿using System;

namespace SocialNetwork2.Model
{
    public class Comment
    {
        public int commentId { get; set; }
        public int postId { get; set; }
        public int userId { get; set; }
        public DateTime commentTimeStamp { get; set; }
        public string commentText { get; set; }


        //setting up 1:n relation with Post
        public virtual Post post { get; set; }

        //setting up 1:n relation with User
        public virtual User user { get; set; }

    }
}
