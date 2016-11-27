using Nancy;
using Nancy.ModelBinding;
using SocialNetwork;
using SocialNetwork.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetworkServerNV1
{
    public class ChatModule : NancyModule
    {
        private FunctionGroup helpers=new FunctionGroup();

        public ChatModule():base("/chat")
        {
            Get["/"] = _ => "Hello, this is chat";
            Post["/send_message"] = parameters => SendMessage(parameters);
            Get["/new_messages"] = parameters => CheckNewMessages(parameters);
        }

        //method used for sending a message
        public dynamic SendMessage(dynamic parameters)
        {
            
            //bind request to object
            var sendQuery = this.Bind<SendQuery>();

            //check user cookie
            if (!helpers.checkToken(sendQuery.userToken))
            {
                return false;
            }

            //check sender ID
            //check receiver ID

            if (!helpers.userExists(sendQuery.senderId) || !helpers.userExists(sendQuery.receiverId))
            {
                return false;
            }

            //push messages into database

            //checking if chat exists. 
            //user id and sender id must be passed in order to check
            //order of passing parameters does not matter
            try
            {
                if (!helpers.chatExists(sendQuery.senderId, sendQuery.receiverId))
                {
                    //creating new chat if one doesn't exist
                    //order of parameters doesn't matter
                    createNewChat(sendQuery.senderId, sendQuery.receiverId);
                }
                //saves message in UnreadMessages table
                saveMessage(sendQuery.messageText, sendQuery.senderId, sendQuery.receiverId, getChatId(sendQuery.senderId, sendQuery.receiverId));
            }
            catch (Exception ex)
            {
                return false;
            }

            //return response code
            return Negotiate.WithStatusCode(200);

        }

        //method used for checking any new received messages
        public dynamic CheckNewMessages(dynamic parameters)
        {
            //todo:
            //check user cookie
            if (!helpers.checkToken(parameters.getToken())) return false;

            //extract from database
            if (!checkUnreadMessages(parameters.userId)) return false;
            List<UnreadMessages> unreadMessages = getAllUnreadMessages(parameters.userId);

            //return a structured model
            return unreadMessages;
            //checks if there is any new entry in unread messages.
            //note: this userId will be recipientId in table ureadMessages



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

        private List<int> getAllUnreadMessagesId(int userId)
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

        private List<UnreadMessages> getAllUnreadMessages(int userId)
        {
            List<int> unreadMessagesIdList = getAllUnreadMessagesId(userId);
            using (var context = new SocialNetworkDBContext())
            {
                List<UnreadMessages> unreadMessages = new List<UnreadMessages>();

                foreach (var messageId in unreadMessagesIdList)
                {
                    unreadMessages.Add((UnreadMessages)context.unreadMessages.Where(u => u.unreadMessageId == messageId));
                }

                return unreadMessages;

            }
        }
    }

    public class SendQuery
    {
        public Token userToken { get; set; }
        public int senderId { get; set; }
        public int receiverId { get; set; }
        public string messageText { get; set; }
    }
}