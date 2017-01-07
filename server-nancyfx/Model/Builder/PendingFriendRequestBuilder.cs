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
    class PendingFriendRequestBuilder
    {
        private int senderId;
        private int receiverId;
        private bool friendRequestTimeStamp;
        private DateTime friendRequestSent;


        public PendingFriendRequestBuilder SenderId(int senderId)
        {
            this.senderId = senderId;
            return this;
        }


        public PendingFriendRequestBuilder ReceiverId(int receiverId)
        {
            this.receiverId = receiverId;
            return this;
        }

        public PendingFriendRequestBuilder FriendRequestConfirmed(bool friendRequestConfirmed)
        {
            this.friendRequestTimeStamp = friendRequestConfirmed;
            return this;
        }

        public PendingFriendRequestBuilder FriendRequestSent(DateTime friendRequestSent)
        {
            this.friendRequestSent = friendRequestSent;
            return this;
        }
           

        public PendingFriendRequest Build()
        {
            return new PendingFriendRequest()
            {
                senderId = senderId,
                receiverId = receiverId,
                friendRequestTimeStamp = friendRequestSent,
                friendRequestConfirmed = friendRequestTimeStamp
            };
        }
    }
}
