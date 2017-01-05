using SocialNetwork2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkServer.Builder
{
    /// <summary>
    /// Class created by Ermin & Ali.
    /// </summary>
    class UnreadMessageBuilder
    {
        private string messageText;
        private int senderId;
        private int recipientId;
        private DateTime messageTimeStamp;
        private int chatId;

        public UnreadMessageBuilder MessageText(string messageText)
        {
            this.messageText = messageText;
            return this;
        }

        public UnreadMessageBuilder SenderId(int senderId)
        {
            this.senderId = senderId;
            return this;
        }

        public UnreadMessageBuilder RecipientId(int recipientId)
        {
            this.recipientId = recipientId;
            return this;
        }

        public UnreadMessageBuilder MessageTimeStamp(DateTime messageTimeStamp)
        {
            this.messageTimeStamp = messageTimeStamp;
            return this;
        }

        public UnreadMessageBuilder ChatId(int chatId)
        {
            this.chatId = chatId;
            return this;
        }

        public UnreadMessage Build()
        {
            return new UnreadMessage()
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
