using SocialNetwork2;
using SocialNetwork2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork2.Controller
{
    /// <summary>
    /// Class used as controller for message related operations and queries.
    /// </summary>
    public static class MessagesController
    {
        /// <summary>
        /// Method used to save message into tables UnreadMessages and PrivateMessages
        /// </summary>
        /// <param name="messageText">string. Text of a message</param>
        /// <param name="senderId">int. Sender's Id</param>
        /// <param name="recipientId">int. Recipient's Id</param>
        /// <param name="chatId">int. Chat's Id</param>
        public static void saveMessage(PrivateMessage message, UnreadMessage unread)
        {
            using (var context = new SocialNetworkDBContext())
            {
                context.unreadMessages.Add(unread);
                context.privateMessages.Add(message);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Method used to check if there are any new entries in table UnreadMessages
        /// </summary>
        /// <param name="userId"> int. User's id</param>
        /// <returns>
        /// Returns true if there are any new entries, else returns false</returns>
        public static bool checkUnreadMessages(int userId)
        {
            using (var context = new SocialNetworkDBContext())
            {
                return context.unreadMessages.Any(um => (um.recipientId == userId));
            }
        }

        /// <summary>
        /// Method used to retrieve id's of all unread messages.
        /// </summary>
        /// <param name="userId">int. User's id.</param>
        /// <returns>
        /// List<int></returns>
        public static List<int> getAllUnreadMessagesId(int userId)
        {
            using (var context = new SocialNetworkDBContext())
            {

                List<int> unreadMessagesIdList = new List<int>();

                var unreadMessages = context.unreadMessages;

                foreach (var um in unreadMessages)
                {
                    if (um.recipientId == userId)
                    {
                        unreadMessagesIdList.Add(um.unreadMessageId);
                    }
                }

                return unreadMessagesIdList;
            }
        }

        /// <summary>
        /// Method used to retrieve and delete all messages from unread messages table
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        /// List<UnreadMessages></returns>
        public static List<UnreadMessage> getAllUnreadMessages(int userId)
        {
            List<int> unreadMessagesIdList = getAllUnreadMessagesId(userId);
            using (var context = new SocialNetworkDBContext())
            {
                List<UnreadMessage> unreadMessages = new List<UnreadMessage>();

                foreach (var messageId in unreadMessagesIdList)
                {
                    //unreadMessages.Add((UnreadMessages)context.unreadMessages.Where(u => u.unreadMessageId == messageId));
                    //extract object by ID
                    unreadMessages.Add(context.unreadMessages.Find(messageId));
                    //remove object from database by ID
                    context.unreadMessages.Remove(context.unreadMessages.Find(messageId));
                    context.SaveChanges();
                }

                return unreadMessages;

            }
        }
    }
}