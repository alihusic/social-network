namespace SocialNetwork2.Request
{
    /// <summary>
    /// Class used to create request that handles picutre editing.
    /// Class created by Ermin & Ali.
    /// </summary>
    public class ChangePictureRequest : ConfidentialRequest
    {
        public string pictureURL { get; set; }
    }
}