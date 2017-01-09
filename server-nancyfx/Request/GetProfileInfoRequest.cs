namespace SocialNetwork2.Request
{
    /// <summary>
    /// Class used to create request that handles loading of profile info.
    /// Class created by Ermin & Ali.
    /// </summary>
    public class GetProfileInfoRequest : ConfidentialRequest
    {
        public int targetId { get; set; }
    }
}