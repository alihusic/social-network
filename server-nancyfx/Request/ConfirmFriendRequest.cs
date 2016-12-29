namespace SocialNetwork2.Request
{
    /// <summary>
    /// Class used to create request that handles confirmation of a friendship :).
    /// </summary>
    public class ConfirmFriendRequest : ConfidentialRequest
    {
        public int senderId { get; set; }
        public int receiverId { get; set; }
    }
}