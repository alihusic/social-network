
using SocialNetwork;
using SocialNetwork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetworkServerNV1
{
    public class ChatFunctionGroup
    {
        //method used for sending a message
        public dynamic Send(dynamic parameters)
        {
            //todo:
            //check user cookie
            //check sender ID
            //check receiver ID


            //push messages into database

            //checking if chat exists. 
            //user id and sender id must be passed in order to check
            //order of passing parameters does not matter
            try
            {
                if (!chatExists(parameters.senderId, parameters.receiverId))
                {
                    //creating new chat if one doesn't exist
                    //order of parameters doesn't matter
                    createNewChat(parameters.senderId, parameters.receiverId);
                }
                //saves message in UnreadMessages table
                saveMessage(parameters.messageText, parameters.senderId, parameters.recipientId, parameters.getChatId());



            }
            catch (Exception ex)
            {
                //e ovdje neka ciba neki exception ko nije mogao napraviti poruku bla bla... mada mislim da su oni svi built in.
                throw;
            }            
            
            //return response code
            return null;
        }

        

        //method used for checking any new received messages
        public dynamic CheckNewMessages(dynamic parameters)
        {
            //todo:
            //check user cookie
            //extract from database
            //return a structured model

            //checks if there is any new entry in unread messages.
            //note: this userId will be recipientId in table ureadMessages
            checkUnreadMessages(parameters.userId);

            
        }

        
        /// <summary>
        /// @getChatId used to retrieve in which chat conversation is happening
        /// </summary>
        /// <param name="user1Id">int. Id of user 1</param>
        /// <param name="user2Id">int. Id of user 2</param>
        /// <returns>
        /// Int which represents chatId of chat between two users</returns>
        private int getChatId(int user1Id, int user2Id)
        {
            using (var context = new SocialNetworkDBContext())
            {
                return context.privateChat.Where(n => (n.user1 == user1Id && n.user2 == user2Id) || (n.user1 == user2Id && n.user2 == user1Id)).First().privateChatId;
            }
               
        }

        
        /// <summary>
        /// @chatExists checks if chat exists
        /// </summary>
        /// <param name="user1Id">int. Id of user 1</param>
        /// <param name="user2Id">int. Id of user 2</param>
        /// <returns></returns>
        private bool chatExists(int user1Id, int user2Id)
        {
            //insert context class name
            using (var context = new SocialNetworkDBContext())
            {
                return context.privateChat.Any(n => (n.user1 == user1Id && n.user2 == user2Id) || (n.user1 == user2Id && n.user2 == user1Id));
            }
        }

        
        /// <summary>
        /// @createNewChat creates new chat.
        /// </summary>
        /// <param name="user1Id">int. Id of a first User</param>
        /// <param name="user2Id">int. Id of a second User</param>
        private void createNewChat(int user1Id, int user2Id)
        {
            using (var context = new SocialNetworkDBContext())
            {
                var chat = new PrivateChat()
                {
                    user1 = user1Id,
                    user2 = user2Id,
                    chatCreationDate = DateTime.Now
                };

                context.privateChat.Add(chat);
                context.SaveChanges();
            }
        }


        
        /// <summary>
        /// @saveMessage saves message into table UnreadMessages and PrivateMessages
        /// </summary>
        /// <param name="messageText">string. Text of a message</param>
        /// <param name="senderId">int. Sender's Id</param>
        /// <param name="recipientId">int. Recipient's Id</param>
        /// <param name="chatId">int. Chat's Id</param>
        private void saveMessage(string messageText, int senderId, int recipientId, int chatId)
        {
            using (var context = new SocialNetworkDBContext())
            {
                var message1 = new UnreadMessages()
                {
                    messageText = messageText,
                    senderId = senderId,
                    recipientId = recipientId,
                    chatID = chatId,
                    messageTimeStamp = DateTime.Now
                };

                var message2 = new PrivateMessages()
                {
                    messageText = messageText,
                    senderId = senderId,
                    recipientId = recipientId,
                    chatID = chatId,
                    messageTimeStamp = DateTime.Now
                };

                context.unreadMessages.Add(message1);
                context.privateMessages.Add(message2);
                context.SaveChanges();
            }
        }

        
        /// <summary>
        /// @checkUnreadMessages checks if there are any new entries in table UnreadMessages
        /// </summary>
        /// <param name="userId"> int. User's id</param>
        /// <returns>
        /// Returns true if there are any new entries, else returns false</returns>
        private bool checkUnreadMessages(int userId)
        {
            using (var context = new SocialNetworkDBContext())
            {
                return context.unreadMessages.Any(um => (um.recipientId == userId));
            }
        }
    }
    

}