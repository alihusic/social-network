using SocialNetwork2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork2.Response
{
    public class ConversationResponse:StatusResponse
    {
        public Conversation conversation { get; set; }
        public ConversationResponse (Conversation conversation):base(true)
        {
            this.conversation = conversation;
        }
    }
}