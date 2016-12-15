using SocialNetwork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace SocialNetworkServer
{
    public class BLOBINA
    {
        public static string s = "This class should be renamed...";
    }

    
    public abstract class SNRequest
    {
        public DateTime timeStamp;
        public string ipAddress;
        public SNRequest()
        {
            timeStamp = DateTime.Now;
            ipAddress = new WebClient().DownloadString("http://icanhazip.com");
        }
    }


    /// <summary>
    /// Class used to separate requests which require user authentication/token.
    /// NOTE: Class is not abstract - can be instantiated due to Liskov Inversion Principle.
    /// Substituting any subclass into its place will work without problems, therefore we
    /// consider it acceptable.
    /// </summary>
    public class ConfidentialRequest : SNRequest
    {
        public Token userToken;
        public ConfidentialRequest():base()
        {

        }
    }

    public class ConfidentialRequestBuilder
    {
        public Token userToken;
        public ConfidentialRequestBuilder UserToken(Token token)
        {
            this.userToken = token;
            return this;
        }
        public SNRequest Build()
        {
            return new ConfidentialRequest
            {
                userToken=userToken
            };
        }
    }

    public class UnrestrictedRequest : SNRequest
    {
        public string crawlerStamp;
    }
    

    /// <summary>
    /// Class used to encapsulate required fields in a send message query
    /// </summary>
    public class MessageSendRequest:ConfidentialRequest
    {
        public int receiverId { get; set; }
        public string messageText { get; set; }
    }

    /// <summary>
    /// Class used to encapsulate required fields in a get all friends query
    /// </summary>
    public class GetAllFriendsRequest:ConfidentialRequest
    {
        public int userId { get; set; }
    }

    /// <summary>
    /// Class used to encapsulate required fields in a delete friend
    /// </summary>
    public class DeleteFriendRequest:ConfidentialRequest
    {
        public int senderId { get; set; }
        public int receiverId { get; set; }
    }

    /// <summary>
    /// Class used to encapsulate required fields in a confirm friend query
    /// </summary>
    public class ConfirmFriendRequest:ConfidentialRequest
    {
        public int senderId { get; set; }
        public int receiverId { get; set; }
    }

    /// <summary>
    /// Class used to encapsulate required fields in a add new friend query
    /// </summary>
    public class AddFriendRequest:ConfidentialRequest
    {
        public int senderId { get; set; }
        public int receiverId { get; set; }
    }

    /// <summary>
    /// Class used to encapsulate required fields in a authentication query
    /// </summary>
    public class AuthenticateUserRequest:UnrestrictedRequest
    {
        public string username { get; set; }
        public string password { get; set; }
    }


    /// <summary>
    /// Class used to encapsulate required fields in a registration query
    /// </summary>
    public class RegisterUserRequest:UnrestrictedRequest
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
    public class CommentCreateRequest:ConfidentialRequest
    {
        public int userId { get; set; }
        public int postId { get; set; }
        public int creatorId { get; set; }
        public int targetId { get; set; }
        public string commentText { get; set; }
    }

    /// <summary>
    /// Class used to encapsulate required fields in a create new post query
    /// </summary>
    public class PostCreateRequest:ConfidentialRequest
    {
        public int targetId { get; set; }
        public int creatorId { get; set; }
        public string postContent { get; set; }
    }

    /// <summary>
    /// Class used to encapsulate required fields in a like post query
    /// </summary>
    public class PostLikeRequest:ConfidentialRequest
    {
        public int userId { get; set; }
        public int creatorId { get; set; }
        public int targetId { get; set; }
        public int postId { get; set; }
    }

    /// <summary>
    /// Class used to encapsulate required fields in a load post query
    /// </summary>
    public class PostLoadRequest:ConfidentialRequest
    {
        public int postId { get; set; }
        public int targetId { get; set; }
        public int creatorId { get; set; }
    }

    /// <summary>
    /// Class used to encapsulate required fields in a newsfeed load query
    /// </summary>
    public class LoadNewsfeedRequest:ConfidentialRequest
    {
        public int interval { get; set; }
    }

    /// <summary>
    /// Class used to encapsulate required fields in a settings edit info query
    /// </summary>

    public class EditUserInfoRequest:ConfidentialRequest
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
    }

    /// <summary>
    /// Class used to encapsulate required fields in a settings change profile picture query
    /// </summary>
    public class ChangePictureRequest:ConfidentialRequest
    {
        public string pictureURL { get; set; }
    }

    /// <summary>
    /// Class used to encapsulate required fields in a settings change password query
    /// </summary>
    public class ChangePasswordRequest:ConfidentialRequest
    {
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }

    /// <summary>
    /// Class used to encapsulate required fields in a get list of users query
    /// Note: user token not required
    /// </summary>
    public class GetListUsersRequest:UnrestrictedRequest
    {
        public List<int> listUserId { get; set; }
    }

    /// <summary>
    /// Class used to encapsulate required fields in a get profile query
    /// </summary>
    public class GetProfileInfoQuery:ConfidentialRequest
    {
        public int targetId { get; set; }
    }

}
