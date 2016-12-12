<<<<<<< HEAD
﻿using SocialNetwork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkServer.Builder
{
    class PrivateMessagesBuilder
    {
        private string messageText;
        private int senderId;
        private int recipientId;
        private DateTime messageTimeStamp;
        private int chatId;


        public PrivateMessagesBuilder MessageText(string messageText)
        {
            this.messageText = messageText;
            return this;
        }

        public PrivateMessagesBuilder SenderId(int senderId)
        {
            this.senderId = senderId;
            return this;
        }

        public PrivateMessagesBuilder RecipientId(int recipientId)
        {
            this.recipientId = recipientId;
            return this;
        }

        public PrivateMessagesBuilder MessageTimeStamp(DateTime messageTimeStamp)
        {
            this.messageTimeStamp = messageTimeStamp;
            return this;
        }

        public PrivateMessagesBuilder ChatId(int chatId)
        {
            this.chatId = chatId;
            return this;
        }

        public PrivateMessages Build()
        {
            return new PrivateMessages()
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
=======
﻿using SocialNetwork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkServer.Builder
{
    class PrivateMessagesBuilder
    {
        private string messageText;
        private int senderId;
        private int recipientId;
        private DateTime messageTimeStamp;
        private int chatId;


        public PrivateMessagesBuilder MessageText(string messageText)
        {
            this.messageText = messageText;
            return this;
        }

        public PrivateMessagesBuilder SenderId(int senderId)
        {
            this.senderId = senderId;
            return this;
        }

        public PrivateMessagesBuilder RecipientId(int recipientId)
        {
            this.recipientId = recipientId;
            return this;
        }

        public PrivateMessagesBuilder MessageTimeStamp(DateTime messageTimeStamp)
        {
            this.messageTimeStamp = messageTimeStamp;
            return this;
        }

        public PrivateMessagesBuilder ChatId(int chatId)
        {
            this.chatId = chatId;
            return this;
        }

        public PrivateMessages Build()
        {
            return new PrivateMessages()
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
>>>>>>> refs/remotes/origin/Maulwurf
