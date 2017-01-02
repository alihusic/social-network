namespace SocialNetwork2.Model
{
    public class Token
    {
        public int tokenId { get; set; }
        public int userId { get; set; }
        public string tokenHash { get; set; }


        //setting up 1:1 relationship with a User
        public virtual User user { get; set; }

    }
}
