namespace SocialNetwork2.Request
{
    /// <summary>
    /// Class used to create requests that don't require tokens.
    /// </summary>
    public class UnrestrictedRequest : SNRequest
    {
        public string crawlerStamp;
    }
}