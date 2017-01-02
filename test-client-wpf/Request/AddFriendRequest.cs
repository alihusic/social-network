namespace SocialNetwork2.Request
{
    /// <summary>
    /// Class used to create requests that handles friendship creation.
    /// </summary>
    public class AddFriendRequest : ConfidentialRequest
    {
        public int senderId { get; set; }
        public int receiverId { get; set; }
    }
}