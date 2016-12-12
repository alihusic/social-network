using SocialNetwork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkServer.Builder
{
    class UnreadMessagesBuilder
    {
        private string messageText;
        private int senderId;
        private int recipientId;
        private DateTime messageTimeStamp;
        private int chatId;

        public UnreadMessagesBuilder MessageText(string messageText)
        {
            this.messageText = messageText;
            return this;
        }

        public UnreadMessagesBuilder SenderId(int senderId)
        {
            this.senderId = senderId;
            return this;
        }

        public UnreadMessagesBuilder RecipientId(int recipientId)
        {
            this.recipientId = recipientId;
            return this;
        }

        public UnreadMessagesBuilder MessageTimeStamp(DateTime messageTimeStamp)
        {
            this.messageTimeStamp = messageTimeStamp;
            return this;
        }

        public UnreadMessagesBuilder ChatId(int chatId)
        {
            this.chatId = chatId;
            return this;
        }

        public UnreadMessages Build()
        {
            return new UnreadMessages()
            {
                messageText = messageText,
                messageTimeStamp = messageTimeStamp,
                senderId = senderId,
                recipientId = recipientId,
                chatID = chatId
            };
        }
    }
}
