namespace SocialNetwork2.Request
{
    /// <summary>
    /// Class used to create request that handles newsfeed loading.
    /// Class created by Ermin & Ali.
    /// </summary>
    public class LoadNewsfeedRequest : ConfidentialRequest
    {
        public int interval { get; set; }
    }
}