namespace SocialNetwork2.Request
{
    /// <summary>
    /// Class used to create request that handles user authentication.
    /// Class created by Ermin & Ali.
    /// </summary>
    public class AuthenticateUserRequest : UnrestrictedRequest
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}