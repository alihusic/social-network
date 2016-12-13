using System;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Model
{
    public class Comments
    {
        [Key]
        public int commentId { get; set; }
        public int postId { get; set; }
        public int userId { get; set; }
        public DateTime commentTimeStamp { get; set; }
        public string commentText { get; set; }


        //1:n with posts
        public virtual Posts post { get; set; }
        //1:n with user
        public virtual User user { get; set; }

    }
}
