namespace SocialNetwork2.Request
{
    /// <summary>
    /// Class used to create request that handles deleting friendship :(.
    /// </summary>
    public class DeleteFriendRequest : ConfidentialRequest
    {
        public int senderId { get; set; }
        public int receiverId { get; set; }
    }
}