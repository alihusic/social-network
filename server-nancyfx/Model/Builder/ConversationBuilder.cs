using SocialNetwork2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork2.Model.Builder
{
    /// <summary>
    /// Class created by Ermin & Ali.
    /// </summary>
    class ConversationBuilder
    {
        private int user1;
        private int user2;
        private int privateChatId;
        private List<PrivateMessage> privateMessages { get; set; }

        public ConversationBuilder User1(int user1)
        {
            this.user1 = user1;
            return this;
        }

        public ConversationBuilder User2(int user2)
        {
            this.user2 = user2;
            return this;
        }

        public ConversationBuilder PrivateChatId(int id)
        {
            this.privateChatId = id;
            return this;
        }

        public ConversationBuilder PrivateMessages(ICollection<PrivateMessage> Imessages)
        {
            List<PrivateMessage> messages = Imessages.ToList();
            this.privateMessages = messages; 
            return this;
        }


        public Conversation Build()
        {
            return new Conversation()
            {
                user1 = user1,
                user2 = user2,
                privateChatId = privateChatId,
                privateMessages=privateMessages
            };
        }

    }
}