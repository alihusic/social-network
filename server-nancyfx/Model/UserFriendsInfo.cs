using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork2.Model
{
    /// <summary>
    /// Class used as model for information about user's friends.
    /// Class created by Ermin.
    /// </summary>
    public class UserFriendsInfo
    {
        public int userId { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public string pictureURL { get; set; }
    }
}
