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
            try
            {
                if (!helpers.userExists(sendQuery.senderId) || !helpers.userExists(sendQuery.receiverId))
                {
                    return false;
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
                if (!helpers.chatExists(sendQuery.senderId, sendQuery.receiverId))
                {
                    //creating new chat if one doesn't exist
                    //order of parameters doesn't matter
                    helpers.createNewChat(sendQuery.senderId, sendQuery.receiverId);
                }
                //saves message in UnreadMessages table
                helpers.saveMessage(sendQuery.messageText, sendQuery.senderId, sendQuery.receiverId, helpers.getChatId(sendQuery.senderId, sendQuery.receiverId));
            }
            catch (Exception ex)
            {
                return "";
            }

            //return response code
            return Negotiate.WithStatusCode(200);

        }

        //method used for checking any new received messages
        public dynamic CheckNewMessages(dynamic parameters)
        {
            //bind request to model
            //check user cookie
            if (!helpers.checkToken(parameters.getToken())) return false;

            //extract from database
            if (!helpers.checkUnreadMessages(parameters.userId)) return false;
            List<UnreadMessages> unreadMessages = helpers.getAllUnreadMessages(parameters.userId);

            //return a structured model
            return unreadMessages;
            //checks if there is any new entry in unread messages.
            //note: this userId will be recipientId in table ureadMessages



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