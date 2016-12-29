using Microsoft.EntityFrameworkCore;
using SocialNetwork2.Model;
using SocialNetwork2.Builder;
using System.Collections.Generic;
using System.Linq;
using TestClientSN.Model;

namespace SocialNetwork2.Controller
{
    /// <summary>
    /// Class used as controller for friend related operations and queries.
    /// </summary>
    public static class FriendsController
    {
        /// <summary>
        /// Method used to check if two users are already friends
        /// </summary>
        /// <param name="user1Id"> int. represents id of first user</param>
        /// <param name="user2Id">int. represents id of second user</param>
        /// <returns>
        /// Returns boolean. If true then friendship exists, if false friendship doesn't exist</returns>
        public static bool friendshipExists(int user1Id, int user2Id)
        {
            using (var context = new SocialNetworkDBContext())
            {
                return context.friendRequest.Any(fr => ((fr.senderId == user1Id && fr.receiverId == user2Id && fr.friendRequestConfirmed == true) || (fr.senderId == user2Id && fr.receiverId == user1Id && fr.friendRequestConfirmed == true)));
            }
        }

        /// <summary>
        /// Method used to check if there exists a friendship request in the database 
        /// </summary>
        /// <param name="user1Id">ID of the first user</param>
        /// <param name="user2Id">ID of the second users</param>
        /// <returns>Returns the truth value of existance</returns>
        public static bool pendingFriendshipRequestExists(int user1Id, int user2Id)
        {
            using (var context = new SocialNetworkDBContext())
            {
                return context.friendRequest.Any(fr => ((fr.senderId == user1Id && fr.receiverId == user2Id && fr.friendRequestConfirmed == false) || (fr.senderId == user2Id && fr.receiverId == user1Id && fr.friendRequestConfirmed == false)));
            }
        }

        /// <summary>
        /// Method used to retrieve id's of all friends user has. 
        /// </summary>
        /// <param name="userId">int. User's Id</param>
        /// <returns>
        /// Returns List<int></returns>
        public static List<int> getAllFriendsId(int userId)
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
        /// Method used to confirm friend request between two users
        /// </summary>
        /// <param name="senderId">int. id of first user</param>
        /// <param name="receiverId">int. ide of second user</param>
        public static void confirmFriendshipRequest(PendingFriendRequest request)
        {
            using (var context = new SocialNetworkDBContext())
            {
                var friendship = context.friendRequest.Where(fr => fr.receiverId == request.receiverId && fr.senderId == request.senderId).FirstOrDefault();
                friendship.friendRequestConfirmed = true;

                bool saveFailed;
                do
                {
                    saveFailed = false;
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        saveFailed = true;
                        var entry = ex.Entries.Single();
                        entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                    }
                } while (saveFailed);
            }
        }

        /// <summary>
        /// Method used to create new pending friendship request
        /// </summary>
        /// <param name="senderId"> int. Sender's Id</param>
        /// <param name="receiverId">int. Receiver's Id</param>
        public static void addNewPendingFriendshipRequest(PendingFriendRequest request)
        {
            using (var context = new SocialNetworkDBContext())
            {

                context.friendRequest.Add(request);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Method used to remove friendship of two users :( tugy plaky
        /// </summary>
        /// <param name="senderId">int. Sender's Id</param>
        /// <param name="receiverId">int. Receiver's Id</param>
        public static void deleteFriendship(int senderId, int receiverId)
        {
            using (var context = new SocialNetworkDBContext())
            {
                var friendship = context.friendRequest.Where(fr => ((fr.senderId == senderId) && (fr.receiverId == receiverId))).First();
                context.friendRequest.Remove(friendship);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Metdod used to find all friends of a user
        /// </summary>
        /// <param name="userId"> int. User's Id</param>
        /// <returns>
        /// Returns List<User></returns>
        public static List<User> getAllFriends(int userId)
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

        /// <summary>
        /// Method used to retrieve information about user's friends.
        /// </summary>
        /// <param name="userIdList">List<int>. List of user id's.</param>
        /// <returns></returns>
        public static List<UserFriendsInfo> getUserFriendsList(List<int> userIdList)
        {
            using (var context = new SocialNetworkDBContext())
            {
                List<UserFriendsInfo> userList = new List<UserFriendsInfo>();
                foreach (var userId in userIdList)
                {
                    var tempUserObject = context.users.Find(userId);
                    userList.Add(new UserFriendsInfoBuilder()
                        .UserId(tempUserObject.userId)
                        .Name(tempUserObject.name)
                        .LastName(tempUserObject.lastName)
                        .PictureURL(tempUserObject.pictureURL)
                        .Build()
                        );

                }

                return userList;
            }
        }

    }
}