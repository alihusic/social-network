using Nancy;
using Nancy.ModelBinding;
using SocialNetwork;
using SocialNetwork.Model;
using SocialNetworkServer;
using SocialNetworkServer.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using TestClientSN.Model;

namespace SocialNetworkServerNV1
{
    /// <summary>
    /// Class inheriting NancyModule class.
    /// Used to handle Friedns-related requests.
    /// </summary>

    public class FriendsModule : NancyModule
    {
        
        /// <summary>
        /// Constructor with route mapping
        /// </summary>

        public FriendsModule():base("/user/friends")
        {
            Get["/"] = _ => "Hello!";
            Post["/add"] = parameters => Add(parameters);
            Post["/confirm"] = parameters => Confirm(parameters);
            Post["/remove"] = parameters => Remove(parameters);
            Post["/get_all"] = parameters => GetAll(parameters);
            Post["/get_list_users"] = parameters => GetListUsers(parameters);
        }

        /// <summary>
        /// Method used to add friends.
        /// </summary>
        /// <param name="parameters">dynamic.</param>
        /// <returns>Status</returns>
        public dynamic Add(dynamic parameters)
        {
            var addQuery = this.Bind<AddFriendRequest>();

            // check token
            if (!TokenFactory.checkToken(addQuery.userToken))
            {
                throw new Exception("Not logged in.");
            }


            if (!UserController.userExists(addQuery.senderId) || !UserController.userExists(addQuery.receiverId))
            {
                throw new Exception("User not found.");
            }

            //check if friendship exists - order of paramaters doesn't matter
            if (!FriendsController.friendshipExists(addQuery.senderId, addQuery.receiverId))
            {
                FriendsController.addNewPendingFriendshipRequest(new PendingFriendRequestBuilder()
                    .FriendRequestConfirmed(false)
                    .SenderId(addQuery.userToken.userId)
                    .ReceiverId(addQuery.receiverId)
                    .FriendRequestSent(DateTime.Now).Build());
            }
            else
            {
                throw new Exception("User already added.");
            }

            /* return status code
            */
            return Negotiate.WithStatusCode(200);
        }

        /// <summary>
        /// Method used to confirm friend request.
        /// </summary>
        /// <param name="parameters">dynamic</param>
        /// <returns>Status</returns>

        public dynamic Confirm(dynamic parameters)
        {
            //bind query
            var confirmQuery = this.Bind<ConfirmFriendRequest>();

            // check token
            if (!TokenFactory.checkToken(confirmQuery.userToken))
            {
                throw new Exception("Not logged in.");
            }

            //check if pending friendship exists- order of parameters doesn't matter
            if (FriendsController.pendingFriendshipRequestExists(confirmQuery.senderId, confirmQuery.receiverId))
            {
                // update database - ova ce ako Bog da raditi. Nemam vremena sad za testiranje detaljno, uglavnom treba sto prije testirati.
                 FriendsController.confirmFriendshipRequest(new PendingFriendRequestBuilder()
                     .SenderId(confirmQuery.senderId)
                     .ReceiverId(confirmQuery.receiverId)
                     .FriendRequestConfirmed(false)
                     .FriendRequestSent(DateTime.Now)
                     .Build());
            }
            else
            {
                throw new Exception("Friendship request does not exist.");
                //neka baci neki exception da ne postoji taj friendship request
            }

            /* return status code
            */
            return "You are now friends!!!";
        }

        /// <summary>
        /// Method used to remove friendship :(
        /// </summary>
        /// <param name="parameters">dynamic</param>
        /// <returns>Status</returns>
        public dynamic Remove(dynamic parameters)
        {
            //bind query
            var deleteQuery = this.Bind<DeleteFriendRequest>();

            // check token
            if (!TokenFactory.checkToken(deleteQuery.userToken))
            {
                throw new Exception("Not logged in.");
            }

            // check if friendship exists
            if (FriendsController.friendshipExists(deleteQuery.senderId, deleteQuery.receiverId))
            {
                //deletes friendship if 2 users are friends
                FriendsController.deleteFriendship(deleteQuery.senderId, deleteQuery.receiverId);
            }
            else
            {
                throw new Exception("Friendship does not exist.");
                //nek mozda baci neki exception da nije pronadjen taj friendship
            }
            /* update database
            * return status code
            */
            return Negotiate.WithStatusCode(200);
        }

        /// <summary>
        /// Method used to retrieve list of all friends id's.
        /// </summary>
        /// <param name="parameters">dynamic</param>
        /// <returns>
        /// List<int></returns>
        public dynamic GetAll(dynamic parameters)
        {
            //bind query
            var getAllQuery = this.Bind<GetAllFriendsRequest>();

            // check token
            if (!TokenFactory.checkToken(getAllQuery.userToken))
            {
                throw new Exception("Not logged in.");
            }


            //List<User> friends = helpers.getAllFriends(getAllQuery.userId);
            List<int> friends = FriendsController.getAllFriendsId(getAllQuery.userToken.userId);
            return friends.Count();
            /* bind the result to a model
            * return model&status code
            */
            return Negotiate.WithStatusCode(200).WithModel(friends);
        }

        /// <summary>
        /// Method used to retrieve list of all friends
        /// </summary>
        /// <param name="parameters">dynamic</param>
        /// <returns>
        /// List<UserFriendsInfo></returns>
        public dynamic GetListUsers(dynamic parameters)
        {
            //bind query to object
            var getListUsersQuery = this.Bind<GetListUsersRequest>();

            List <UserFriendsInfo> users= FriendsController.getUserFriendsList(getListUsersQuery.listUserId);

            return users;
        }

        /// <summary>
        /// Method used to retrieve information on User
        /// </summary>
        /// <param name="parameters">dynamic.</param>
        /// <returns>
        /// Object of type ProfileInfo</returns>
        public dynamic GetProfileInfo(dynamic parameters)
        {
            //bind query to object
            var getProfileInfoQuery = this.Bind<GetProfileInfoQuery>();

            // check token
            if (!TokenFactory.checkToken(getProfileInfoQuery.userToken))
            {
                throw new Exception("Not logged in.");
            }

            if (!UserController.userExists(getProfileInfoQuery.targetId))
            {
                throw new Exception("User not found.");
            }

            if (!FriendsController.getAllFriendsId(getProfileInfoQuery.userToken.userId).Contains(getProfileInfoQuery.targetId))
            {
                throw new Exception("User profile not visible.");
            }

            ProfileInfo profileInfo = UserController.getProfileInfo(getProfileInfoQuery.targetId);
            

            //return profileInfo;
            return null;
        }

        /*todo: 
         * add a status report class
         */

    }

    
}