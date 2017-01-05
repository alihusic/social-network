namespace SocialNetwork2.Request
{
    /// <summary>
    /// Class used to create request that handles comment creation.
    /// Class created by Ermin & Ali.
    /// </summary>
    public class CommentCreateRequest : ConfidentialRequest
    {
        public int userId { get; set; }
        public int postId { get; set; }
        public int creatorId { get; set; }
        public int targetId { get; set; }
        public string commentText { get; set; }
    }
}