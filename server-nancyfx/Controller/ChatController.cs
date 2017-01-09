using SocialNetwork2.Model;
using System.Linq;

namespace SocialNetwork2.Controller
{
    /// <summary>
    /// Class used as controller for chat related operations and queries.
    /// Class created by Ermin & Ali.
    /// </summary>
    public static class ChatController
    {
        /// <summary>
        /// Method used to check if chat exists
        /// </summary>
        /// <param name="user1Id">int. Id of user 1</param>
        /// <param name="user2Id">int. Id of user 2</param>
        /// <returns>
        /// Returns true if chat exists, else returns false</returns>
        public static bool chatExists(int user1Id, int user2Id)
        {
            //insert context class name
            using (var context = new SocialNetworkDBContext())
            {
                return context.privateChat.Any(n => (n.user1 == user1Id && n.user2 == user2Id) || (n.user1 == user2Id && n.user2 == user1Id));
            }
        }


        /// <summary>
        /// Method used to retrieve ID of PrivateChat in which conversation is happening
        /// </summary>
        /// <param name="user1Id">int. Id of user 1</param>
        /// <param name="user2Id">int. Id of user 2</param>
        /// <returns>
        /// Int which represents chatId of chat between two users</returns>
        public static int getChatId(int user1Id, int user2Id)
        {
            using (var context = new SocialNetworkDBContext())
            {
                return context.privateChat.Where(n => (n.user1 == user1Id && n.user2 == user2Id) || (n.user1 == user2Id && n.user2 == user1Id)).First().privateChatId;
            }

        }

        /// <summary>
        /// Method used to retrieve PrivateChat object by ID
        /// </summary>
        /// <param name="chatId">Chat ID</param>
        /// <returns></returns>
        public static PrivateChat getChatById(int chatId)
        {
            using(var context = new SocialNetworkDBContext())
            {
                return context.privateChat.Find(chatId);
            }
        }

        /// <summary>
        /// Method used to create new chat.
        /// </summary>
        /// <param name="user1Id">int. Id of a first User</param>
        /// <param name="user2Id">int. Id of a second User</param>
        public static void createNewChat(PrivateChat chat)
        {
            using (var context = new SocialNetworkDBContext())
            {
                context.privateChat.Add(chat);
                context.SaveChanges();
            }
        }
    }

    
}