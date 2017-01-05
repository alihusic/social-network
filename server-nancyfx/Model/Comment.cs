using System;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork2.Model
{
    /// <summary>
    /// Class used as database model for Comment table.
    /// Class created by Ermin.
    /// </summary>
    public class Comment
    {
        [Key]
        public int commentId { get; set; }
        public int postId { get; set; }
        public int userId { get; set; }
        public DateTime commentTimeStamp { get; set; }
        public string commentText { get; set; }


        //setting up 1:n relation with Posts
        public virtual Post post { get; set; }

        //setting up 1:n relation with User
        public virtual User user { get; set; }

    }
}
