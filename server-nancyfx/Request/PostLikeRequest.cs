namespace SocialNetwork2.Request
{
    /// <summary>
    /// Class used to create request that handles like operations.
    /// </summary>
    public class PostLikeRequest : ConfidentialRequest
    {
        public int userId { get; set; }
        public int creatorId { get; set; }
        public int targetId { get; set; }
        public int postId { get; set; }
    }
}