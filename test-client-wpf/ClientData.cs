using SocialNetworkServer.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestClientSN.Model;
using SocialNetworkServer;
using Newtonsoft.Json;
using testClientWPF;
using SocialNetwork2.Model;

namespace SocialNetwork
{
    public sealed class ClientInfo
    {
        private static readonly ClientInfo instance = new ClientInfo();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static ClientInfo()
        {
        }

        private ClientInfo()
        {
        }

        public static ClientInfo Instance
        {
            get
            {
                return instance;
            }
        }


        //info about friends
        public List<int> friendsIdList
        {
            get
            {
                return this.friendsIdList;
            }
            set
            {
                lock (((ICollection)friendsIdList).SyncRoot)
                {
                    friendsIdList = value;
                }
            }
        }

        public List<UserFriendsInfo> friendsThumbList
        {
            get
            {
                return this.friendsThumbList;
            }
            set
            {
                lock (((ICollection)friendsThumbList).SyncRoot)
                {
                    friendsThumbList = value;
                }
            }
        }


        //info about profiles
        public ProfileInfo userProfile
        {
            get
            {
                return this.userProfile;
            }
            set
            {
                lock (((ICollection)userProfile).SyncRoot)
                {
                    userProfile = value;
                }
            }
        }

        

        public ProfileInfo currentProfile
        {
            get
            {
                return this.currentProfile;
            }
            set
            {
                lock (((ICollection)currentProfile).SyncRoot)
                {
                    currentProfile = value;
                }
            }
        }

        //session token
        public Token sessionToken
        {
            get
            {
                return this.sessionToken;
            }
            set
            {
                lock (((ICollection)sessionToken).SyncRoot)
                {
                    sessionToken = value;
                }
            }
        }

        //info about conversations
        //TODO make conversation object
        public List<object> conversationList
        {
            get
            {
                return this.conversationList;
            }
            set
            {
                lock (((ICollection)conversationList).SyncRoot)
                {
                    conversationList = value;
                }
            }
        }

        //info about Notification
        public List<Notification> notificationList
        {
            get
            {
                return this.notificationList;
            }
            set
            {
                lock (((ICollection)notificationList).SyncRoot)
                {
                    notificationList = value;
                }
            }
        }

        //info about post lists
        //TODO: pack comments with Post
        //either fix lazy loading or make request
        public List<Post> newsfeedPostList
        {
            get
            {
                return this.newsfeedPostList;
            }
            set
            {
                lock (((ICollection)newsfeedPostList).SyncRoot)
                {
                    newsfeedPostList = value;
                }
            }
        }

        public List<Post> currentProfilePostList
        {
            get
            {
                return this.currentProfilePostList;
            }
            set
            {
                lock (((ICollection)currentProfilePostList).SyncRoot)
                {
                    currentProfilePostList = value;
                }
            }
        }

        //info about new messages
        public List<PrivateMessage> unreadMessages
        {
            get
            {
                return this.unreadMessages;
            }
            set
            {
                lock (((ICollection)unreadMessages).SyncRoot)
                {
                    unreadMessages = value;
                }
            }
        }
        

    }
}
