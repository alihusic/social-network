using SocialNetwork2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetworkServerNV1.Response
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