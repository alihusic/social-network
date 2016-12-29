namespace SocialNetwork2.Request
{
    /// <summary>
    /// Class used to create request that handles newsfeed loading.
    /// </summary>
    public class LoadNewsfeedRequest : ConfidentialRequest
    {
        public int interval { get; set; }
    }
}