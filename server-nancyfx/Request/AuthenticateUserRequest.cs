namespace SocialNetwork2.Request
{
    /// <summary>
    /// Class used to create request that handles user authentication.
    /// </summary>
    public class AuthenticateUserRequest : UnrestrictedRequest
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}