using SocialNetwork2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkServer.Builder
{
    class PrivateMessageBuilder
    {
        private string messageText;
        private int senderId;
        private int recipientId;
        private DateTime messageTimeStamp;
        private int chatId;


        public PrivateMessageBuilder MessageText(string messageText)
        {
            this.messageText = messageText;
            return this;
        }

        public PrivateMessageBuilder SenderId(int senderId)
        {
            this.senderId = senderId;
            return this;
        }

        public PrivateMessageBuilder RecipientId(int recipientId)
        {
            this.recipientId = recipientId;
            return this;
        }

        public PrivateMessageBuilder MessageTimeStamp(DateTime messageTimeStamp)
        {
            this.messageTimeStamp = messageTimeStamp;
            return this;
        }

        public PrivateMessageBuilder ChatId(int chatId)
        {
            this.chatId = chatId;
            return this;
        }

        public PrivateMessage Build()
        {
            return new PrivateMessage()
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
