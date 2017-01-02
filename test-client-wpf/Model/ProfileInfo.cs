using System;

namespace SocialNetwork2.Model
{
    public class ProfileInfo
    {
        public int userId { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public string username { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string pictureURL { get; set; }
        public string coverPictureURL { get; set; }
        public string gender { get; set; }
        public DateTime dateOfBirth { get; set; }
    }
}
