using Newtonsoft.Json;
using SocialNetwork2.Model;
using SocialNetwork2.Request;
using SocialNetworkServer;
using SocialNetworkServerNV1.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestClientSN.Model;

namespace testClientWPF
{
    public class ServiceConnector
    {
        private static bool isResponseError(string responseString)
        {
            StatusResponse responseObject = JsonConvert.DeserializeObject<StatusResponse>(responseString);
            return !responseObject.isSuccessful;
        }

        private static Exception generateExceptionFromResponse(string responseString)
        {
            var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(responseString);
            return new Exception(errorResponse.errorMessage);
        }

        private static string generateMessageFromResponse(string responseString)
        {
            MessageResponse responseObject = JsonConvert.DeserializeObject<MessageResponse>(responseString);
            return responseObject.responseMessage;
        }

        public Token authenticate(AuthenticateUserRequest requestInfo)
        {
            string requestBody = JsonConvert.SerializeObject(requestInfo);

            var request = new SNServiceRequestBuilder()
                .Accept("application/json")
                .ContentType("application/json")
                .RequestBody(requestBody)
                .RequestMethod("POST")
                .UrlSubPath("/user/authenticate")
                .Build();

            var responseString = request.requestFromServer();

            if (isResponseError(responseString))
            {

                throw generateExceptionFromResponse(responseString);
            }
            return JsonConvert.DeserializeObject<TokenResponse>(responseString).userToken;
        }

        public bool logOut(ConfidentialRequest requestInfo)
        {
            string requestBody = JsonConvert.SerializeObject(requestInfo);

            var request = new SNServiceRequestBuilder()
                .Accept("application/json")
                .ContentType("application/json")
                .RequestBody(requestBody)
                .RequestMethod("POST")
                .UrlSubPath("/user/log_out")
                .Build();

            var responseString = request.requestFromServer();
            //throw new Exception(responseString);
            if (isResponseError(responseString))
            {
                throw generateExceptionFromResponse(responseString);
            }
            //return true;
            return JsonConvert.DeserializeObject<StatusResponse>(responseString).isSuccessful;
        }

        public List<UnreadMessage> checkNewMessages(ConfidentialRequest requestInfo)
        {

            string requestBody = JsonConvert.SerializeObject(requestInfo);

            var request = new SNServiceRequestBuilder()
                .Accept("application/json")
                .ContentType("application/json")
                .RequestBody(requestBody)
                .RequestMethod("POST")
                .UrlSubPath("/chat/new_messages")
                .Build();

            var responseString = request.requestFromServer();

            if (isResponseError(responseString))
            {
                throw generateExceptionFromResponse(responseString);
            }

            UnreadMessageListResponse listMessages = JsonConvert.DeserializeObject<UnreadMessageListResponse>(responseString);

            return listMessages.unreadMessageList;
        }

        public string sendMessage(MessageSendRequest requestInfo)
        {

            string requestBody = JsonConvert.SerializeObject(requestInfo);

            var request = new SNServiceRequestBuilder()
                .Accept("application/json")
                .ContentType("application/json")
                .RequestBody(requestBody)
                .RequestMethod("POST")
                .UrlSubPath("/chat/send_message")
                .Build();

            var responseString = request.requestFromServer();

            if (isResponseError(responseString))
            {
                throw generateExceptionFromResponse(responseString);
            }

            return generateMessageFromResponse(responseString);
        }

        public string createPost(PostCreateRequest requestInfo)
        {

            string requestBody = JsonConvert.SerializeObject(requestInfo);

            var request = new SNServiceRequestBuilder()
                .Accept("application/json")
                .ContentType("application/json")
                .RequestBody(requestBody)
                .RequestMethod("POST")
                .UrlSubPath("/post/create")
                .Build();

            var responseString = request.requestFromServer();
            if (isResponseError(responseString))
            {
                throw generateExceptionFromResponse(responseString);
            }

            return generateMessageFromResponse(responseString);


        }

        public string addFriend(AddFriendRequest requestInfo)
        {
            string requestBody = JsonConvert.SerializeObject(requestInfo);

            var request = new SNServiceRequestBuilder()
                .Accept("application/json")
                .ContentType("application/json")
                .RequestBody(requestBody)
                .RequestMethod("POST")
                .UrlSubPath("/user/friends/add")
                .Build();

            var responseString = request.requestFromServer();
            if (isResponseError(responseString))
            {
                throw generateExceptionFromResponse(responseString);
            }
            return generateMessageFromResponse(responseString);

        }

        public List<UserFriendsInfo> getUserInfoFromIdList(List<int> idList)
        {
            GetListUsersRequest requestInfo = new GetListUsersRequest
            {
                listUserId = idList
            };

            string requestBody = JsonConvert.SerializeObject(requestInfo);

            var request = new SNServiceRequestBuilder()
                .Accept("application/json")
                .ContentType("application/json")
                .RequestBody(requestBody)
                .RequestMethod("POST")
                .UrlSubPath("/user/friends/get_list_users")
                .Build();

            var responseString = request.requestFromServer();

            if (isResponseError(responseString))
            {
                throw generateExceptionFromResponse(responseString);
            }

            return JsonConvert.DeserializeObject<UserFriendsInfoListResponse>(responseString).userFriendsInfoList;


        }

        public List<UserFriendsInfo> getAllFriendsInfo(ConfidentialRequest requestInfo)
        {
            string requestBody = JsonConvert.SerializeObject(requestInfo);

            var request = new SNServiceRequestBuilder()
                .Accept("application/json")
                .ContentType("application/json")
                .RequestBody(requestBody)
                .RequestMethod("POST")
                .UrlSubPath("/user/friends/get_all")
                .Build();

            var responseString = request.requestFromServer();

            if (isResponseError(responseString))
            {
                throw generateExceptionFromResponse(responseString);
            }

            List<int> allFriendsIdList = JsonConvert.DeserializeObject<IdListResponse>(responseString).idList;



            return getUserInfoFromIdList(allFriendsIdList);
            
    }

        public string confirmFriend(ConfirmFriendRequest requestInfo)
        {
            string requestBody = JsonConvert.SerializeObject(requestInfo);

            var request = new SNServiceRequestBuilder()
                .Accept("application/json")
                .ContentType("application/json")
                .RequestBody(requestBody)
                .RequestMethod("POST")
                .UrlSubPath("/user/friends/confirm")
                .Build();

            var responseString = request.requestFromServer();
            if (isResponseError(responseString))
            {
                throw generateExceptionFromResponse(responseString);
            }
            return generateMessageFromResponse(responseString);
        }

        public string removeFriend(DeleteFriendRequest requestInfo)
        {
            string requestBody = JsonConvert.SerializeObject(requestInfo);

            var request = new SNServiceRequestBuilder()
                .Accept("application/json")
                .ContentType("application/json")
                .RequestBody(requestBody)
                .RequestMethod("POST")
                .UrlSubPath("/user/friends/remove")
                .Build();

            var responseString = request.requestFromServer();
            if (isResponseError(responseString))
            {
                throw generateExceptionFromResponse(responseString);
            }
            return generateMessageFromResponse(responseString);
        }

        public List<Post> loadNewsfeed(LoadNewsfeedRequest requestInfo)
        {
            string requestBody = JsonConvert.SerializeObject(requestInfo);

            var request = new SNServiceRequestBuilder()
                .Accept("application/json")
                .ContentType("application/json")
                .RequestBody(requestBody)
                .RequestMethod("POST")
                .UrlSubPath("/newsfeed/load")
                .Build();

            var responseString = request.requestFromServer();


            if (isResponseError(responseString))
            {
                throw generateExceptionFromResponse(responseString);
            }

            return JsonConvert.DeserializeObject<PostListResponse>(responseString).postList;
        }

        public string registerUser(RegisterUserRequest requestInfo)
        {
            string requestBody = JsonConvert.SerializeObject(requestInfo);

            var request = new SNServiceRequestBuilder()
                .Accept("application/json")
                .ContentType("application/json")
                .RequestBody(requestBody)
                .RequestMethod("POST")
                .UrlSubPath("/user/register")
                .Build();

            var responseString = request.requestFromServer();
            if (isResponseError(responseString))
            {
                throw generateExceptionFromResponse(responseString);
            }
            return generateMessageFromResponse(responseString);
        }

    }
}
