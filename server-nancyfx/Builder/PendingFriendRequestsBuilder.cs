using SocialNetwork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkServer.Builder
{
    class PendingFriendRequestsBuilder
    {
        private int senderId;
        private int receiverId;
        private bool friendRequestConfirmed;
        private DateTime friendRequestSent;


        public PendingFriendRequestsBuilder SenderId(int senderId)
        {
            this.senderId = senderId;
            return this;
        }


        public PendingFriendRequestsBuilder ReceiverId(int receiverId)
        {
            this.receiverId = receiverId;
            return this;
        }

        public PendingFriendRequestsBuilder FriendRequestConfirmed(bool friendRequestConfirmed)
        {
            this.friendRequestConfirmed = friendRequestConfirmed;
            return this;
        }

        public PendingFriendRequestsBuilder FriendRequestSent(DateTime friendRequestSent)
        {
            this.friendRequestSent = friendRequestSent;
            return this;
        }
           

        public PendingFriendRequests Build()
        {
            return new PendingFriendRequests()
            {
                senderId = senderId,
                receiverId = receiverId,
                friendRequestSent = friendRequestSent,
                friendRequestConfirmed = friendRequestConfirmed
            };
        }
    }
}
