using Nancy;
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
            var msg = new Message
            {
                senderId = 2,
                receiverId = 3,
                content = "pozdrav"
            };
            return msg;
        }
    }
    //to be deleted only here for testing purposes
    public class Message
    {
        public int senderId { get; set; }
        public int receiverId { get; set; }
        public string content { get; set; }

    }

}