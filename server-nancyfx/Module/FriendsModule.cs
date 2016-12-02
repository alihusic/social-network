using Nancy;
using Nancy.ModelBinding;
using SocialNetwork;
using SocialNetwork.Model;
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
                return false;
            }


            if (!helpers.userExists(addQuery.senderId) || !helpers.userExists(addQuery.receiverId))
            {
                return false;
            }

            //check if friendship exists - order of paramaters doesn't matter
            if (!helpers.friendshipExists(addQuery.senderId, addQuery.receiverId))
            {
                helpers.addNewPendingFriendshipRequest(addQuery.senderId, addQuery.receiverId);
            }
            else
            {
                return false;
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
                return false;
            }
            //check if pending friendship exists- order of parameters doesn't matter
            if (helpers.pendingFriendshipRequestExists(confirmQuery.senderId, confirmQuery.receiverId))
            {
                // update database - ova ce ako Bog da raditi. Nemam vremena sad za testiranje detaljno, uglavnom treba sto prije testirati.
                helpers.confirmFriendshipRequest(confirmQuery.senderId, confirmQuery.receiverId);
            }
            else
            {
                return false;
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
                return false;
            }
            // check if friendship exists
            if (helpers.friendshipExists(deleteQuery.senderId, deleteQuery.receiverId))
            {
                //deletes friendship if 2 users are friends
                helpers.deleteFriendship(deleteQuery.senderId, deleteQuery.receiverId);
            }
            else
            {
                return false;
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
                return false;
            }

            //e jebes ga, ovo bi trebalo radit
            List<User> friends = helpers.getAllFriends(getAllQuery.userId);

            /* bind the result to a model
            * return model&status code
            */
            return Negotiate.WithStatusCode(200).WithModel(friends);
        }

        /*todo: 
         * maybe add a check new friendship requests method
         * add a check user cookie method
         * add a check if friendship exists method - done (Ermin)
         * add a check if pending friendship exists method
         * add a status report class
         */



    }

    class GetAllQuery
    {
        public int userId { get; set; }
        public Token userToken { get; set; }
    }

    class DeleteQuery
    {
        public int senderId { get; set; }
        public int receiverId { get; set; }
        public Token userToken { get; set; }
    }

    class ConfirmQuery
    {
        public int senderId { get; set; }
        public int receiverId { get; set; }
        public Token userToken { get; set; }
    }

    class AddQuery
    {
        public int senderId { get; set; }
        public int receiverId { get; set; }
        public Token userToken { get; set; }
    }
}