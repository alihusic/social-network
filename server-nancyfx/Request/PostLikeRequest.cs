namespace SocialNetwork2.Request
{
    /// <summary>
    /// Class used to create request that handles like operations.
    /// Class created by Ermin & Ali.
    /// </summary>
    public class PostLikeRequest : ConfidentialRequest
    {
        public int creatorId { get; set; }
        public int targetId { get; set; }
        public int postId { get; set; }
    }
}