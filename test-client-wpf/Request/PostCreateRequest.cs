namespace SocialNetwork2.Request
{
    /// <summary>
    /// Class used to create request that handles post creation.
    /// </summary>
    public class PostCreateRequest : ConfidentialRequest
    {
        public int targetId { get; set; }
        public int creatorId { get; set; }
        public string postContent { get; set; }
    }
}