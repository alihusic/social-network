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

        public FriendsModule()
        {
            Get["/"] = _ => "Hello!";
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
                addNewPendingFriendshipRequest(addQuery.senderId, addQuery.receiverId);
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
                confirmFriendshipRequest(confirmQuery.senderId, confirmQuery.receiverId);
            }
            else
            {
                return false;
                //neka baci neki exception da ne postoji taj friendship request
            }

            /* return status code
            */
            return null;
        }

        //method used to delete a person from the friend list
        public dynamic Delete(dynamic parameters)
        {
            /*todo:
             * check user cookie*/
            // check if friendship exists
            if (helpers.friendshipExists(parameters.senderId, parameters.receiverId))
            {
                //deletes friendship if 2 users are friends
                deleteFriendship(parameters.senderId, parameters.receiverId);
            }
            else
            {
                //nek mozda baci neki exception da nije pronadjen taj friendship
            }
            /* update database
            * return status code
            */
            return null;
        }

        //method used to get a list of all people on the friend list
        public dynamic GetAll(dynamic parameters)
        {
            /*todo:
             * check user cookie
            */

            //e jebes ga, ovo bi trebalo radit
            List<User> friends = getAllFriends(parameters.userId);

            /* bind the result to a model
            * return model&status code
            */
            return null;
        }

        /*todo: 
         * maybe add a check new friendship requests method
         * add a check user cookie method
         * add a check if friendship exists method - done (Ermin)
         * add a check if pending friendship exists method
         * add a status report class
         */







        /// <summary>
        /// @confirmFriendRequest confirms friend request between two users
        /// </summary>
        /// <param name="senderId">int. id of first user</param>
        /// <param name="receiverId">int. ide of second user</param>
        protected void confirmFriendshipRequest(int senderId, int receiverId)
        {
            using (var context = new SocialNetworkDBContext())
            {
                var friendRequest = new PendingFriendRequests()
                {
                    senderId = senderId,
                    receiverId = receiverId,
                    friendRequestConfirmed = true
                };

                context.friendRequest.Attach(friendRequest);
                context.Entry(friendRequest).Property(fr => fr.friendRequestConfirmed).IsModified = true;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// @addNewFriendship creates new pending friendship request
        /// </summary>
        /// <param name="senderId"> int. Sender's Id</param>
        /// <param name="receiverId">int. Receiver's Id</param>
        protected void addNewPendingFriendshipRequest(int senderId, int receiverId)
        {
            using (var context = new SocialNetworkDBContext())
            {
                var newFriendship = new PendingFriendRequests()
                {
                    senderId = senderId,
                    receiverId = receiverId,
                    friendRequestSent = DateTime.Now,
                    friendRequestConfirmed = false
                };

                context.friendRequest.Add(newFriendship);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// @deleteFriendship removes friendship of two users :(
        /// </summary>
        /// <param name="senderId">int. Sender's Id</param>
        /// <param name="receiverId">int. Receiver's Id</param>
        protected void deleteFriendship(int senderId, int receiverId)
        {
            using (var context = new SocialNetworkDBContext())
            {
                var friendship = context.friendRequest.Where(fr => ((fr.senderId == senderId) && (fr.receiverId == receiverId))).First();
                context.friendRequest.Remove(friendship);
            }
        }

        /// <summary>
        /// @getAllFriendsId is used to find id's of all friends user has
        /// </summary>
        /// <param name="userId">int. User's Id</param>
        /// <returns>
        /// Returns List<int></returns>
        protected List<int> getAllFriendsId(int userId)
        {
            using (var context = new SocialNetworkDBContext())
            {

                List<int> friendsList = new List<int>();

                var friends = context.friendRequest;

                foreach (var f in friends)
                {
                    if (f.receiverId == userId && f.friendRequestConfirmed == true)
                    {
                        friendsList.Add(f.senderId);
                    }
                    else if (f.senderId == userId && f.friendRequestConfirmed == true)
                    {
                        friendsList.Add(f.receiverId);
                    }
                }

                return friendsList;
            }

        }

        /// <summary>
        /// @getAllUsers Used to find all friends of a user
        /// </summary>
        /// <param name="userId"> int. User's Id</param>
        /// <returns>
        /// Returns List<User></returns>
        protected List<User> getAllFriends(int userId)
        {
            List<int> friendsId = getAllFriendsId(userId);
            using (var context = new SocialNetworkDBContext())
            {
                List<User> userFriends = new List<User>();

                foreach (var i in friendsId)
                {
                    userFriends.Add((User)context.users.Where(u => u.userId == i));
                }

                return userFriends;

            }
        }

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