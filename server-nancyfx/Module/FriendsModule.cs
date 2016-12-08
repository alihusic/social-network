using Nancy;
using Nancy.ModelBinding;
using SocialNetwork;
using SocialNetwork.Model;
using SocialNetworkServer;
using SocialNetworkServer.Builder;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetworkServerNV1
{
    public class FriendsModule : NancyModule
    {
        private FunctionGroup helpers = new FunctionGroup();

        public FriendsModule():base("/user/friends")
        {
            Get["/"] = _ => "Hello!";
            Get["/add"] = parameters => Add(parameters);
            Get["/confirm"] = parameters => Confirm(parameters);
            Get["/remove"] = parameters => Remove(parameters);
            Get["/get_all"] = parameters => GetAll(parameters);

        }

        //method used to add a friend, not very obvious
        public dynamic Add(dynamic parameters)
        {
            var addQuery = this.Bind<AddQuery>();

            // check token
            if (!helpers.checkToken(addQuery.userToken))
            {
                throw new Exception("Not logged in.");
            }


            if (!helpers.userExists(addQuery.senderId) || !helpers.userExists(addQuery.receiverId))
            {
                throw new Exception("User not found.");
            }

            //check if friendship exists - order of paramaters doesn't matter
            if (!helpers.friendshipExists(addQuery.senderId, addQuery.receiverId))
            {
                helpers.addNewPendingFriendshipRequest(new PendingFriendRequestsBuilder()
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

        //method used to confirm a friend request
        public dynamic Confirm(dynamic parameters)
        {
            //bind query
            var confirmQuery = this.Bind<ConfirmQuery>();

            // check token
            if (!helpers.checkToken(confirmQuery.userToken))
            {
                throw new Exception("Not logged in.");
            }

            //check if pending friendship exists- order of parameters doesn't matter
            if (helpers.pendingFriendshipRequestExists(confirmQuery.senderId, confirmQuery.receiverId))
            {
                // update database - ova ce ako Bog da raditi. Nemam vremena sad za testiranje detaljno, uglavnom treba sto prije testirati.
                helpers.confirmFriendshipRequest(new PendingFriendRequestsBuilder()
                    .SenderId(confirmQuery.senderId)
                    .ReceiverId(confirmQuery.receiverId)
                    .FriendRequestConfirmed(true)
                    .Build());
            }
            else
            {
                throw new Exception("Friendship request does not exist.");
                //neka baci neki exception da ne postoji taj friendship request
            }

            /* return status code
            */
            return Negotiate.WithStatusCode(200);
        }

        //method used to delete a person from the friend list
        public dynamic Remove(dynamic parameters)
        {
            //bind query
            var deleteQuery = this.Bind<DeleteQuery>();

            // check token
            if (!helpers.checkToken(deleteQuery.userToken))
            {
                throw new Exception("Not logged in.");
            }

            // check if friendship exists
            if (helpers.friendshipExists(deleteQuery.senderId, deleteQuery.receiverId))
            {
                //deletes friendship if 2 users are friends
                helpers.deleteFriendship(deleteQuery.senderId, deleteQuery.receiverId);
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

        //method used to get a list of all people on the friend list
        public dynamic GetAll(dynamic parameters)
        {
            //bind query
            var getAllQuery = this.Bind<GetAllQuery>();

            // check token
            if (!helpers.checkToken(getAllQuery.userToken))
            {
                throw new Exception("Not logged in.");
            }

            //e jebes ga, ovo bi trebalo radit
            List<User> friends = helpers.getAllFriends(getAllQuery.userId);

            /* bind the result to a model
            * return model&status code
            */
            return Negotiate.WithStatusCode(200).WithModel(friends);
        }

        /*todo: 
         * add a status report class
         */

    }

    
}