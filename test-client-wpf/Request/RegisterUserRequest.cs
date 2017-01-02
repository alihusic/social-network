using System;

namespace SocialNetwork2.Request
{
    /// <summary>
    /// Class used to create request that handles user registration.
    /// </summary>
    public class RegisterUserRequest : UnrestrictedRequest
    {
        public string name { get; set; }
        public string lastName { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string pictureURL { get; set; }
        public string coverPictureURL { get; set; }
        public string gender { get; set; }
        public DateTime dateOfBirth { get; set; }
    }
}