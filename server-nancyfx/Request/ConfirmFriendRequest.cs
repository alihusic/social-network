namespace SocialNetwork2.Request
{
    /// <summary>
    /// Class used to create request that handles confirmation of a friendship :).
    /// Class created by Ermin & Ali.
    /// </summary>
    public class ConfirmFriendRequest : ConfidentialRequest
    {
        public int senderId { get; set; }
        public int receiverId { get; set; }
    }
}