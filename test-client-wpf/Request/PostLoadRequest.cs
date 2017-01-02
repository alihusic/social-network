namespace SocialNetwork2.Request
{
    /// <summary>
    /// Class used to create request that handles loading of posts.
    /// </summary>
    public class PostLoadRequest : ConfidentialRequest
    {
        public int postId { get; set; }
        public int targetId { get; set; }
        public int creatorId { get; set; }
    }
}