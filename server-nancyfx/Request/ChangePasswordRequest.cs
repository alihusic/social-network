namespace SocialNetwork2.Request
{
    /// <summary>
    /// Class used to create request that handles password editing.
    /// Class created by Ermin & Ali.
    /// </summary>
    public class ChangePasswordRequest : ConfidentialRequest
    {
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }
}