using Nancy;
using Nancy.ModelBinding;
using SocialNetwork2.Model;
using SocialNetwork2.Controller;
using SocialNetwork2.Factory;
using SocialNetwork2.Request;
using SocialNetworkServer.Builder;
using System;
using System.Collections.Generic;
using SocialNetworkServerNV1.Response;

namespace SocialNetwork2
{
    /// <summary>
    /// Class inheriting NancyModule class.
    /// Used to handle Chat-related requests.
    /// Class created by Ermin & Ali.
    /// </summary>
    public class ChatModule : NancyModule
    {
        

        /// <summary>
        /// Constructor with route mapping
        /// </summary>
        public ChatModule():base("/chat")
        {
            Get["/"] = _ => "Hello, this is chat";
            Post["/send_message"] = parameters => SendMessage(parameters);
            Post["/new_messages"] = parameters => CheckNewMessages(parameters);
            Post["/load_conversation"] = parameters => LoadConversation(parameters);
        }

        
        /// <summary>
        /// Method used for sending a message
        /// </summary>
        /// <param name="parameters">Request parameters</param>
        /// <returns>Successfulnes of operation</returns>
        public dynamic SendMessage(dynamic parameters)
        {
            //bind request to object
            var sendQuery = this.Bind<MessageSendRequest>();
            
            //check user cookie
            if (!TokenFactory.checkToken(sendQuery.userToken))
            {
                return new ErrorResponse("You must log in first.");
            }

            //check sender ID
            //check receiver ID
            try
            {
                if (!UserController.userExists(sendQuery.userToken.userId) || !UserController.userExists(sendQuery.receiverId))
                {
                    return new ErrorResponse("Invalid receiver ID.");
                    //throw new Exception("Invalid receiver ID"); ;
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
                if (!ChatController.chatExists(sendQuery.userToken.userId, sendQuery.receiverId))
                {
                    //creating new chat if one doesn't exist
                    //order of parameters doesn't matter
                    ChatController.createNewChat(new PrivateChatBuilder().User1(sendQuery.userToken.userId)
                        .User2(sendQuery.receiverId)
                        .ChatCreationDate(DateTime.Now).Build());
                }
                
                //saves message in UnreadMessages and PrivateMessages tables
                MessagesController.saveMessage(new PrivateMessageBuilder()
                    .SenderId(sendQuery.userToken.userId)
                    .RecipientId(sendQuery.receiverId)
                    .MessageTimeStamp(DateTime.Now)
                    .MessageText(sendQuery.messageText)
                    .ChatId(ChatController.getChatId(sendQuery.userToken.userId,sendQuery.receiverId)).Build(), 
                    new UnreadMessageBuilder()
                    .SenderId(sendQuery.userToken.userId)
                    .RecipientId(sendQuery.receiverId)
                    .MessageTimeStamp(DateTime.Now)
                    .MessageText(sendQuery.messageText)
                    .ChatId(ChatController.getChatId(sendQuery.userToken.userId, sendQuery.receiverId)).Build());
            }
            catch (Exception ex)
            {
                return new ErrorResponse("Something went horribly wrong on serverside 0.0");
            }

            //return response code
            return new MessageResponse("Message sent successfully");
        }

        
        /// <summary>
        /// Method used for checking new received messages
        /// </summary>
        /// <param name="parameters">Request parameters</param>
        /// <returns>Response with messages</returns>
        public dynamic CheckNewMessages(dynamic parameters)
        {
            //bind request to model
            var checkNewMessagesRequest = this.Bind<ConfidentialRequest>();

            //check user cookie
            if (!TokenFactory.checkToken(checkNewMessagesRequest.userToken))
            {
                return new ErrorResponse("You must log in first.");
            }

            //extract from database
            if (!MessagesController.checkUnreadMessages(checkNewMessagesRequest.userToken.userId))
            {
                return new ErrorResponse("No new messages.");
            }
            List<UnreadMessage> unreadMessages = MessagesController.getAllUnreadMessages(checkNewMessagesRequest.userToken.userId);

            //return a structured model
            return new UnreadMessageListResponse(unreadMessages);
            //checks if there is any new entry in unread messages.
            //note: this userId will be recipientId in table ureadMessages

        }

        public dynamic LoadConversation(dynamic parameters)
        {
            //bind request to model
            var loadConversationRequest = this.Bind<GetProfileInfoRequest>();

            //check user cookie
            if (!TokenFactory.checkToken(loadConversationRequest.userToken))
            {
                return new ErrorResponse("You must log in first.");
            }

            PrivateChat chat = ChatController.getChatById(
                ChatController.getChatId(loadConversationRequest.userToken.userId,
                                            loadConversationRequest.targetId));

            Conversation conversation = new ConversationBuilder()
                .PrivateChatId(chat.privateChatId)
                .PrivateMessages(chat.privateMessages)
                .User1(chat.user1)
                .User2(chat.user2)
                .Build();


            return new ConversationResponse(conversation);

        }

    }

    

}