using SocialNetwork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetworkServer
{
    public class BLOBINA
    {
        public static string s = "This class should be renamed...";
    }

    /// <summary>
    /// Class used to encapsulate required fields in a send message query
    /// </summary>
    public class SendQuery
    {
        public Token userToken { get; set; }
        public int receiverId { get; set; }
        public string messageText { get; set; }
    }

    /// <summary>
    /// Class used to encapsulate required fields in a check new messages query
    /// </summary>
    public class CheckNewMessagesQuery
    {
        public Token userToken { get; set; }
    }

    /// <summary>
    /// Class used to encapsulate required fields in a get all friends query
    /// </summary>
    class GetAllQuery
    {
        public Token userToken { get; set; }
    }

    /// <summary>
    /// Class used to encapsulate required fields in a delete friend
    /// </summary>
    class DeleteQuery
    {
        public int senderId { get; set; }
        public int receiverId { get; set; }
        public Token userToken { get; set; }
    }

    /// <summary>
    /// Class used to encapsulate required fields in a confirm friend query
    /// </summary>
    class ConfirmQuery
    {
        public int senderId { get; set; }
        public int receiverId { get; set; }
        public Token userToken { get; set; }
    }

    /// <summary>
    /// Class used to encapsulate required fields in a add new friend query
    /// </summary>
    class AddQuery
    {
        public int senderId { get; set; }
        public int receiverId { get; set; }
        public Token userToken { get; set; }
    }

    /// <summary>
    /// Class used to encapsulate required fields in a authentication query
    /// </summary>
    public class AuthenticateQuery
    {
        public string username { get; set; }
        public string password { get; set; }
    }


    /// <summary>
    /// Class used to encapsulate required fields in a registration query
    /// </summary>
    public class RegisterQuery
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

    /// <summary>
    /// Class used to encapsulate required fields in a comment query
    /// </summary>
    class CommentQuery
    {
        public int userId { get; set; }
        public int postId { get; set; }
        public int creatorId { get; set; }
        public int targetId { get; set; }
        public string commentText { get; set; }
        public Token userToken { get; set; }
    }

    /// <summary>
    /// Class used to encapsulate required fields in a create new post query
    /// </summary>
    class CreateQuery
    {
        public int targetId { get; set; }
        public int creatorId { get; set; }
        public Token userToken { get; set; }
        public string postContent { get; set; }
    }

    /// <summary>
    /// Class used to encapsulate required fields in a like post query
    /// </summary>
    class LikeQuery
    {
        public int userId { get; set; }
        public int creatorId { get; set; }
        public int targetId { get; set; }
        public int postId { get; set; }
        public Token userToken { get; set; }
    }

    /// <summary>
    /// Class used to encapsulate required fields in a load post query
    /// </summary>
    class LoadQuery
    {
        public Token userToken { get; set; }
        public int postId { get; set; }
        public int targetId { get; set; }
        public int creatorId { get; set; }
        public DateTime postCreationDate { get; set; }
    }

    /// <summary>
    /// Class used to encapsulate required fields in a newsfeed load query
    /// </summary>
    class LoadNewsfeedQuery
    {
        public Token userToken { get; set; }
        public int interval { get; set; }
    }

    /// <summary>
    /// Class used to encapsulate required fields in a settings edit info query
    /// </summary>

    class EditInfoQuery
    {
        public string name { get; set; }
        public string lastName { get; set; }
        public string username { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string pictureURL { get; set; }
        public string coverPictureURL { get; set; }
        public string gender { get; set; }
        public DateTime dateOfBirth { get; set; }
        public Token userToken { get; set; }
    }

    /// <summary>
    /// Class used to encapsulate required fields in a settings change profile picture query
    /// </summary>
    class ChangeProfilePictureQuery
    {
        public string username { get; set; }
        public string pictureURL { get; set; }
        public Token userToken { get; set; }
    }

    /// <summary>
    /// Class used to encapsulate required fields in a settings change password query
    /// </summary>
    class ChangePasswordQuery
    {
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
        public Token userToken { get; set; }
    }


    /// <summary>
    /// Class used to encapsulate required fields in a settings change cover picture query
    /// </summary>
    class ChangeCoverPictureQuery
    {
        public string coverPictureURL { get; set; }
        public Token userToken { get; set; }
    }

    /// <summary>
    /// Class used to encapsulate required fields in a notifications query
    /// </summary>
    public class NotificationQuery
    {
        public Token userToken { get; set; }
    }

    /// <summary>
    /// Class used to encapsulate required fields in a log out query
    /// </summary>

    public class LogOutQuery
    {
        public Token userToken { get; set; }
    }

    /// <summary>
    /// Class used to encapsulate required fields in a get user's info query
    /// </summary>
    public class LoadUserInfoQuery
    {
        public Token userToken { get; set; }
    }
}
