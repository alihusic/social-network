using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocialNetwork;
using SocialNetwork.Model;

namespace SocialNetworkServerNV1
{
    public class FriendsFunctionGroup
    {
        //method used to add a friend, not very obvious
        public dynamic Add(dynamic parameters)
        {
            /*todo:
             * check user cookie- jos se nismo dogovorili kako cemo ovo implementirati, listom ili iz baze provjeravati, pa nkea ga zasad(Ermin)
             */
            // if user exists - or could handle with exception later 
            userExists(parameters.senderId);
            userExists(parameters.receiverId);

            //check if friendship exists - order of paramaters doesn't matter
            if(!friendshipExists(parameters.senderId, parameters.receiverId))
            {
                addNewPendingFriendshipRequest(parameters.senderId, parameters.receiverId);
            }
            else
            {
                //neka baci exception da vec postoji friendship request
            }

            /* return status code
            */
            return null;
        }


        //method used to confirm a friend request
        public dynamic Confirm(dynamic parameters)
        {
            /*todo:
             * check user cookie
            */
            //check if pending friendship exists- order of parameters doesn't matter
            if(pendingFriendshipRequestExists(parameters.senderId, parameters.receiverId))
            {
                // update database - ova ce ako Bog da raditi. Nemam vremena sad za testiranje detaljno, uglavnom treba sto prije testirati.
                confirmFriendshipRequest(parameters.senderID, parameters.receiverId);
            } else
            {
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
            if (friendshipExists(parameters.senderId, parameters.receiverId))
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
        /// @userExists checks if there is user with certain Id
        /// </summary>
        /// <param name="userId"> Type int. Users id that is sent to the method. </param>
        /// <returns>
        /// Returns boolean. True if user exists, false if doesn't.</returns>

        private bool userExists(int userId)
        {
            using (var context = new SocialNetworkDBContext())
            {
                return context.users.Any(u => (u.userId == userId));
            }

        }

        /// <summary>
        /// @friendshipExists checks if two users are already friends
        /// </summary>
        /// <param name="user1Id"> int. represents id of first user</param>
        /// <param name="user2Id">int. represents id of second user</param>
        /// <returns>
        /// Returns boolean. If true then friendship exists, if false friendship doesn't exist</returns>

        private bool friendshipExists(int user1Id, int user2Id)
        {
            using (var context = new SocialNetworkDBContext())
            {
                return context.friendRequest.Any(fr => ((fr.senderId == user1Id && fr.receiverId == user2Id && fr.friendRequestConfirmed == true) || (fr.senderId == user2Id && fr.receiverId == user1Id && fr.friendRequestConfirmed == true)));
            }
        }

        /// <summary>
        /// @pendingFriendshipRequest
        /// </summary>
        /// <param name="user1Id"></param>
        /// <param name="user2Id"></param>
        /// <returns></returns>

        private bool pendingFriendshipRequestExists(int user1Id, int user2Id)
        {
            using (var context = new SocialNetworkDBContext())
            {
                return context.friendRequest.Any(fr => ((fr.senderId == user1Id && fr.receiverId == user2Id && fr.friendRequestConfirmed == false) || (fr.senderId == user2Id && fr.receiverId == user1Id && fr.friendRequestConfirmed == false)));
            }
        }

        /// <summary>
        /// @confirmFriendRequest confirms friend request between two users
        /// </summary>
        /// <param name="senderId">int. id of first user</param>
        /// <param name="receiverId">int. ide of second user</param>

        private void confirmFriendshipRequest(int senderId, int receiverId)
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

        private void addNewPendingFriendshipRequest(int senderId, int receiverId)
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

        private void deleteFriendship(int senderId, int receiverId)
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

        private List<int> getAllFriendsId(int userId)
        {
            using (var context = new SocialNetworkDBContext())
            {

                List<int> friendsList = new List<int>();

                var friends = context.friendRequest;
                
                foreach (var f in friends)
                {
                    if(f.receiverId == userId  && f.friendRequestConfirmed == true)
                    {
                        friendsList.Add(f.senderId);
                    }
                    else if(f.senderId == userId && f.friendRequestConfirmed == true)
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
        private List<User> getAllFriends(int userId)
        {
            List<int> friendsId = getAllFriendsId(userId);
            using (var context = new SocialNetworkDBContext())
            {
                List<User> userFriends = new List<User>();

                foreach (var i in friendsId)
                {
                    userFriends.Add((User)context.users.Where(u => u.userId == (int)i));
                }

                return userFriends;

            }    
        }
    }
}
