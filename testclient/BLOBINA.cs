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

    /// <summary>
    /// Class used as base class for all requests.
    /// </summary>
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


    /// <summary>
    /// Class used to create requests that require tokens.
    /// </summary>
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

    /// <summary>
    /// Class used to create requests that don't require tokens.
    /// </summary>
    public class UnrestrictedRequest : SNRequest
    {
        public string crawlerStamp;
    }
    

    /// <summary>
    /// Class used to create request that handles message sending.
    /// </summary>
    public class MessageSendRequest:ConfidentialRequest
    {
        public int receiverId { get; set; }
        public string messageText { get; set; }
    }

    /// <summary>
    /// Class used to create request that handles friend extraction.
    /// </summary>
    public class GetAllFriendsRequest:ConfidentialRequest
    {
        public int userId { get; set; }
    }

    /// <summary>
    /// Class used to create request that handles deleting friendship :(.
    /// </summary>
    public class DeleteFriendRequest:ConfidentialRequest
    {
        public int senderId { get; set; }
        public int receiverId { get; set; }
    }

    /// <summary>
    /// Class used to create request that handles confirmation of a friendship :).
    /// </summary>
    public class ConfirmFriendRequest:ConfidentialRequest
    {
        public int senderId { get; set; }
        public int receiverId { get; set; }
    }

    /// <summary>
    /// Class used to create requests that handles friendship creation.
    /// </summary>
    public class AddFriendRequest:ConfidentialRequest
    {
        public int senderId { get; set; }
        public int receiverId { get; set; }
    }

    /// <summary>
    /// Class used to create request that handles user authentication.
    /// </summary>
    public class AuthenticateUserRequest:UnrestrictedRequest
    {
        public string username { get; set; }
        public string password { get; set; }
    }


    /// <summary>
    /// Class used to create request that handles user registration.
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
    /// Class used to create request that handles comment creation.
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
    /// Class used to create request that handles post creation.
    /// </summary>
    public class PostCreateRequest:ConfidentialRequest
    {
        public int targetId { get; set; }
        public int creatorId { get; set; }
        public string postContent { get; set; }
    }

    /// <summary>
    /// Class used to create request that handles like operations.
    /// </summary>
    public class PostLikeRequest:ConfidentialRequest
    {
        public int userId { get; set; }
        public int creatorId { get; set; }
        public int targetId { get; set; }
        public int postId { get; set; }
    }

    /// <summary>
    /// Class used to create request that handles loading of posts.
    /// </summary>
    public class PostLoadRequest:ConfidentialRequest
    {
        public int postId { get; set; }
        public int targetId { get; set; }
        public int creatorId { get; set; }
    }

    /// <summary>
    /// Class used to create request that handles newsfeed loading.
    /// </summary>
    public class LoadNewsfeedRequest:ConfidentialRequest
    {
        public int interval { get; set; }
    }

    /// <summary>
    /// Class used to create request that handles editing of user's info.
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
    /// Class used to create request that handles picutre editing.
    /// </summary>
    public class ChangePictureRequest:ConfidentialRequest
    {
        public string pictureURL { get; set; }
    }

    /// <summary>
    /// Class used to create request that handles password editing.
    /// </summary>
    public class ChangePasswordRequest:ConfidentialRequest
    {
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }

    /// <summary>
    /// Class used to create request that handles loading of user id's.
    /// Note: user token not required
    /// </summary>
    public class GetListUsersRequest:UnrestrictedRequest
    {
        public List<int> listUserId { get; set; }
    }

    /// <summary>
    /// Class used to create request that handles loading of profile info.
    /// </summary>
    public class GetProfileInfoQuery:ConfidentialRequest
    {
        public int targetId { get; set; }
    }

}
