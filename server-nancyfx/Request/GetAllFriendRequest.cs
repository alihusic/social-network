namespace SocialNetwork2.Request
{

    /// <summary>
    /// Class used to create request that handles friend extraction.
    /// Class created by Ermin & Ali.
    /// </summary>
    public class GetAllFriendRequest : ConfidentialRequest
    {
        public int userId { get; set; }
    }
}