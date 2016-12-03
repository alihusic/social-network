using Nancy;
using Nancy.ModelBinding;
using SocialNetwork;
using SocialNetwork.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetworkServerNV1
{
    /// <summary>
    /// Class inheriting NancyModule class.
    /// Used to handle Chat-related requests.
    /// </summary>
    public class ChatModule : NancyModule
    {
        private FunctionGroup helpers=new FunctionGroup();

        /// <summary>
        /// Constructor with route mapping
        /// </summary>
        public ChatModule():base("/chat")
        {
            Get["/"] = _ => "Hello, this is chat";
            Post["/send_message"] = parameters => SendMessage(parameters);
            Post["/new_messages"] = parameters => CheckNewMessages(parameters);
        }

        
        /// <summary>
        /// Method used for sending a message
        /// </summary>
        /// <param name="parameters">Request parameters</param>
        /// <returns>Successfulnes of operation</returns>
        public dynamic SendMessage(dynamic parameters)
        {
            
            //bind request to object
            var sendQuery = this.Bind<SendQuery>();

            
            //check user cookie
            if (!helpers.checkToken(sendQuery.userToken))
            {
                throw new Exception("Your token is: " + sendQuery.userToken.tokenHash);
                //throw new Exception("You must log in");
            }



            //check sender ID
            //check receiver ID
            try
            {
                if (!helpers.userExists(sendQuery.userToken.userId) || !helpers.userExists(sendQuery.receiverId))
                {
                    throw new Exception("Invalid receiver ID"); ;
                }
            }catch(Exception e)
            {
                throw e;
            }
            

            //push messages into database

            //checking if chat exists. 
            //user id and sender id must be passed in order to check
            //order of passing parameters does not matter
            try
            {
                if (!helpers.chatExists(sendQuery.userToken.userId, sendQuery.receiverId))
                {
                    //creating new chat if one doesn't exist
                    //order of parameters doesn't matter
                    helpers.createNewChat(sendQuery.userToken.userId, sendQuery.receiverId);
                }
                //saves message in UnreadMessages table
                helpers.saveMessage(sendQuery.messageText, sendQuery.userToken.userId, sendQuery.receiverId, helpers.getChatId(sendQuery.userToken.userId, sendQuery.receiverId));
            }
            catch (Exception ex)
            {
                return "";
            }

            //return response code
            return Negotiate.WithStatusCode(200);

        }

        
        /// <summary>
        /// Method used for checking new received messages
        /// </summary>
        /// <param name="parameters">Request parameters</param>
        /// <returns>Response with messages</returns>
        public dynamic CheckNewMessages(dynamic parameters)
        {
            //bind request to model
            var checkNewMessagesQuery = this.Bind<CheckNewMessagesQuery>();

            //check user cookie
            if (!helpers.checkToken(checkNewMessagesQuery.userToken)) throw new Exception("You must log in");

            //extract from database
            if (!helpers.checkUnreadMessages(checkNewMessagesQuery.userToken.userId)) throw new Exception("No new messages");
            List<UnreadMessages> unreadMessages = helpers.getAllUnreadMessages(checkNewMessagesQuery.userToken.userId);

            //return a structured model
            return unreadMessages;
            //checks if there is any new entry in unread messages.
            //note: this userId will be recipientId in table ureadMessages

        }

    }

    /// <summary>
    /// Class used to encapsulate required fields in a send message query
    /// </summary>
    public class SendQuery
    {
        public Token userToken { get; set; }
        public int receiverId { get; set; }
        public string messageText { get; set; }
    }

    /// <summary>
    /// Class used to encapsulate required fields in a check new messages query
    /// </summary>
    public class CheckNewMessagesQuery
    {
        public Token userToken { get; set; }
    }

}