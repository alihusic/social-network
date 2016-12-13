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


        //setting up 1:n relation with Posts
        public virtual Posts post { get; set; }
        //setting up 1:n relation with User
        public virtual User user { get; set; }

    }
}
