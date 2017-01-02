using Newtonsoft.Json;
using SocialNetwork2.Model;
using SocialNetwork2.Request;
using SocialNetworkServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestClientSN.Model;

namespace testClientWPF
{
    public class ServiceConnector
    {
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

            //statusLabel.Text = responseString;
            var tempToken = JsonConvert.DeserializeObject<Token>(responseString);

            if (tempToken.tokenHash != null && tempToken.tokenHash.Length == 40)
            {
                return tempToken;
            }
            throw new Exception("Invalid login.");
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
            try
            {
                var responseString = request.requestFromServer();
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong");
            }
            return true;
        }

        public List<PrivateMessage> checkNewMessages(ConfidentialRequest requestInfo)
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

            List<PrivateMessage> listMessages = JsonConvert.DeserializeObject<List<PrivateMessage>>(responseString);

            return listMessages;
        }

        public bool sendMessage(MessageSendRequest requestInfo)
        {
            try
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
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool createPost(PostCreateRequest requestInfo)
        {
            try
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
                return true;
            } catch (Exception e)
            {
                return false;
            }

        }

        public bool addFriend(AddFriendRequest requestInfo)
        {
            try
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

                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public List<UserFriendsInfo> getAllFriendsInfo(ConfidentialRequest requestInfo)
        {
            try
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

                return JsonConvert.DeserializeObject<List<UserFriendsInfo>>(responseString);
            } catch(Exception e)
            {
                throw new Exception("Something went wrong.\n"+e.Message);
            }
            
    }

        public bool confirmFriend(ConfirmFriendRequest requestInfo)
        {
            try
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
                return true;
            }catch(Exception e)
            {
                return false;
            }
            
        }

        public bool removeFriend(DeleteFriendRequest requestInfo)
        {
            try
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
                return true;
            }catch (Exception e)
            {
                return false;
            }
        }

        public List<Post> loadNewsfeed(LoadNewsfeedRequest requestInfo)
        {
            try
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


                if (responseString == null) throw new Exception("No more Post!");
                return JsonConvert.DeserializeObject<List<Post>>(responseString);
            }
            catch(Exception e)
            {
                throw new Exception("No more Post!");
            }
        }

        public bool registerUser(RegisterUserRequest requestInfo)
        {
            try
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
                return true;
            }catch(Exception e)
            {
                return false;
            }
        }
    }
}
