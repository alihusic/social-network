using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork2.Model.Builder
{
    /// <summary>
    /// Class created by Ermin & Ali.
    /// </summary>
    public class UserFriendsInfoBuilder
    {
        private int userId { get; set; }
        private string name { get; set; }
        private string lastName { get; set; }
        private string pictureURL { get; set; }

        public UserFriendsInfoBuilder UserId(int userId)
        {
            this.userId = userId;
            return this;
        }
        public UserFriendsInfoBuilder Name(string name)
        {
            this.name = name;
            return this;
        }
        public UserFriendsInfoBuilder LastName(string lastName)
        {
            this.lastName = lastName;
            return this;
        }
        public UserFriendsInfoBuilder PictureURL(string pictureURL)
        {
            this.pictureURL = pictureURL;
            return this;
        }
        public UserFriendsInfo Build()
        {
            return new UserFriendsInfo
            {
                userId=this.userId,
                name=this.name,
                lastName=this.lastName,
                pictureURL=this.pictureURL
            };
        }
    }
}