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
            userExists(senderId);
            userExists(receiverId);

            //check if friendship exists - order of paramaters doesn't matter
            friendshipExists(senderId, receiverId);


            //check if pending friendship exists- order of parameters doesn't matter
            pendingFriendshipRequestExists(senderId, receiverId);


            // update database - ova ce ako Bog da raditi. Nemam vremena sad za testiranje detaljno, uglavnom treba sto prije testirati.
            confirmFriendshipRequest(senderID, receiverId);


            /* return status code
            */
            return null;
        }




        //method used to confirm a friend request
        public dynamic Confirm(dynamic parameters)
        {
            /*todo:
             * check user cookie
             * check if pending friendship exists
             * update database
             * return status code
             */
            return null;
        }

        //method used to delete a person from the friend list
        public dynamic Delete(dynamic parameters)
        {
            /*todo:
             * check user cookie
             * check if friendship exists
             * update database
             * return status code
             */
            return null;
        }

        //method used to get a list of all people on the friend list
        public dynamic GetAll(dynamic parameters)
        {
            /*todo:
             * check user cookie
             * simple database query
             * bind the result to a model
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
                return context.friendRequest.Any(fr => ((fr.senderId == user1Id && fr.receiverId == user2Id && fr.firendRequestConfirmed == true) || (fr.senderId == user2Id && fr.receiverId == user1Id && fr.firendRequestConfirmed == true)));
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
                return context.friendRequest.Any(fr => ((fr.senderId == user1Id && fr.receiverId == user2Id && fr.firendRequestConfirmed == false) || (fr.senderId == user2Id && fr.receiverId == user1Id && fr.firendRequestConfirmed == false)));
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
                    firendRequestConfirmed = true
                };

                context.friendRequest.Attach(friendRequest);
                context.Entry(friendRequest).Property(fr => fr.firendRequestConfirmed).IsModified = true;
                context.SaveChanges();
            }
        }
    }

    


}
