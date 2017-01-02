namespace SocialNetwork2.Request
{
    /// <summary>
    /// Class used to create request that handles loading of profile info.
    /// </summary>
    public class GetProfileInfoQuery : ConfidentialRequest
    {
        public int targetId { get; set; }
    }
}