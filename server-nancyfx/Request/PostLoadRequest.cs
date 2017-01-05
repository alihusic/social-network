namespace SocialNetwork2.Request
{
    /// <summary>
    /// Class used to create request that handles loading of posts.
    /// Class created by Ermin & Ali.
    /// </summary>
    public class PostLoadRequest : ConfidentialRequest
    {
        public int postId { get; set; }
        public int targetId { get; set; }
        public int creatorId { get; set; }
    }
}