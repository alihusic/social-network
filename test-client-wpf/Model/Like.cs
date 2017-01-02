namespace SocialNetwork2.Model
{
    //model for table Likes
    //will be used to store likes info

    public class Like
    {
        public int likeId { get; set; }
        public int userId { get; set; }
        public int postId { get; set; }


        //setting up 1:n relation with User
        public virtual User user { get; set; }

        //setting up 1:n relation with Post
        public virtual Post Post { get; set; }
        
    }
}
