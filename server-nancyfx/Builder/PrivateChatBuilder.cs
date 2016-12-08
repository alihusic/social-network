using SocialNetwork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkServer.Builder
{
    class PrivateChatBuilder
    {
        private int user1;
        private int user2;
        private DateTime chatCreationDate;

        public PrivateChatBuilder User1(int user1)
        {
            this.user1 = user1;
            return this;
        }

        public PrivateChatBuilder User2(int user2)
        {
            this.user2 = user2;
            return this;
        }

        public PrivateChatBuilder ChatCreationDate(DateTime chatCreationDate)
        {
            this.chatCreationDate = chatCreationDate;
            return this;
        }

        public PrivateChat Build()
        {
            return new PrivateChat()
            {
                user1 = user1,
                user2 = user2,
                chatCreationDate = chatCreationDate
            };
        }

    }
}
