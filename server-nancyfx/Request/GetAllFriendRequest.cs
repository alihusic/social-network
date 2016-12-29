namespace SocialNetwork2.Request
{

    /// <summary>
    /// Class used to create request that handles friend extraction.
    /// </summary>
    public class GetAllFriendRequest : ConfidentialRequest
    {
        public int userId { get; set; }
    }
}