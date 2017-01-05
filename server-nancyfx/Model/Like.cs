using System.ComponentModel.DataAnnotations;

namespace SocialNetwork2.Model
{
    /// <summary>
    /// Class used as database model for Like table.
    /// Class created by Ermin.
    /// </summary>

    public class Like
    {
        [Key]
        public int likeId { get; set; }
        public int userId { get; set; }
        public int postId { get; set; }


        //setting up 1:n relation with User
        public virtual User user { get; set; }

        //setting up 1:n relation with Posts
        public virtual Post posts { get; set; }
        
    }
}
