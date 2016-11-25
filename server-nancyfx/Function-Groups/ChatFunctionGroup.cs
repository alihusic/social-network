
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
                if (!chatExists(senderId, receiverId))
                {
                    //creating new chat if one doesn't exist
                    //order of parameters doesn't matter
                    createNewChat(senderId, receiverId);
                }
                //saves message in UnreadMessages table
                saveMessage(messageText, senderId, recipientId, getChatId());



            }
            catch (Exception ex)
            {
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
            checkUnreadMessages(userId);

            return msg;
        }

        /* @getChatId returns chat ID of 2 chat users
            
            @param

            int user1Id - Id of first user
            int user2Id - Id of second user

            note: parameter order(who is sender, who is receiver) does not matter
        */

        private int getChatId(int user1Id, int user2Id)
        {
            using (var context = new SocialNetworkDBContext())
            {
                return context.privateChat.Where(n => (n.user1 == user1Id && n.user2 == user2Id) || (n.user1 == user2Id && n.user2 == user1Id)).First().privateChatId;
            }
               
        }

        /* @chatExists returns true if chat exists, returns false if chat does not exist
            
            @param

            int user1Id - Id of first user
            int user2Id - Id of second user

            note: parameter order(who is sender, who is receiver) does not matter
        */

        private bool chatExists(int user1Id, int user2Id)
        {
            //insert context class name
            using (var context = new SocialNetworkDBContext())
            {
                return context.privateChat.Any(n => (n.user1 == user1Id && n.user2 == user2Id) || (n.user1 == user2Id && n.user2 == user1Id));
            }
        }

        /* @createNewChat creates new chat
            
            @param

            int user1Id - Id of first user
            int user2Id - Id of second user

            note: parameter order(who is sender, who is receiver) does not matter
        */

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


        /* @saveMessage saves message into table UnreadMessages and PrivateMessages
            
            @param

            string messageText- text of a message
            int senderId - person who sent message
            int recipientId - person who receives message
            int chatId - chat in which communication is happening
        */

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

        /* @checkUnreadMessages returns true if there is new entry in unreadMessages table, else false
            
            @param

            int userId - Id of user who receives message
        */

        private bool checkUnreadMEssages(int userId)
        {
            using (var context = new SocialNetworkDBContext())
            {
                return context.unreadMessages.Any(um => (um.recipientId == userId));
            }
        }
    }
    

}