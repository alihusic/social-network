namespace SocialNetwork2.Request
{
    /// <summary>
    /// Class used to create request that handles picutre editing.
    /// </summary>
    public class ChangePictureRequest : ConfidentialRequest
    {
        public string pictureURL { get; set; }
    }
}