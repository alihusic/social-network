namespace SocialNetwork2.Request
{
    /// <summary>
    /// Class used to create request that handles password editing.
    /// </summary>
    public class ChangePasswordRequest : ConfidentialRequest
    {
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }
}