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
        private static volatile ClientInfo instance = new ClientInfo();
        private static object syncRoot = new Object();
        

        private ClientInfo()
        {
            _newsFeedPostList = new List<Post>();
            _friendsIdList = new List<int>();
            _friendsThumbList = new List<UserFriendsInfo>();
            _conversationList = new List<object>();
            _notificationList = new List<Notification>();
            _currentProfilePostList = new List<Post>();
            _unreadMessagesList = new List<UnreadMessage>();
        }


        public static ClientInfo Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new ClientInfo();
                    }
                }
                return instance;
            }
        }


        //info about friends
        private List<int> _friendsIdList;
        public List<int> FriendsIdList
        {
            get
            {
                return _friendsIdList;
            }
            set
            {
                if (_friendsIdList == null)
                {
                    _friendsIdList = value;
                }
                else lock (syncRoot)
                {
                    _friendsIdList = value;
                }
            }
        }

        private List<UserFriendsInfo> _friendsThumbList;
        public List<UserFriendsInfo> FriendsThumbList
        {
            get
            {
                return _friendsThumbList;
            }
            set
            {
                if (_friendsThumbList == null)
                {
                    _friendsThumbList = value;
                }
                else lock (syncRoot)
                {
                    _friendsThumbList = value;
                }
            }
        }


        //info about profiles
        private ProfileInfo _userProfile;
        public ProfileInfo UserProfile
        {
            get
            {
                return _userProfile;
            }
            set
            {
                if (_userProfile == null)
                {
                    _userProfile = value;
                }
                else lock (syncRoot)
                {
                    _userProfile = value;
                }
            }
        }


        private ProfileInfo _currentProfile;
        public ProfileInfo CurrentProfile
        {
            get
            {
                return _currentProfile;
            }
            set
            {
                if (_currentProfile == null)
                {
                    _currentProfile = value;
                }
                else lock (syncRoot)
                {
                    _currentProfile = value;
                }
            }
        }

        //session token

        private Token _sessionToken;
        public Token SessionToken
        {
            get
            {
                return _sessionToken;
            }
            set
            {
                if (_sessionToken == null)
                {
                    _sessionToken = value;
                }
                else lock(syncRoot)
                {
                    _sessionToken = value;
                }
            }
        }

        //info about conversations
        //TODO make conversation object
        private List<object> _conversationList;
        public List<object> ConversationList
        {
            get
            {
                return _conversationList;
            }
            set
            {
                if (_conversationList == null)
                {
                    _conversationList = value;
                }
                else lock (syncRoot)
                {
                    _conversationList = value;
                }
            }
        }

        //info about Notification
        private List<Notification> _notificationList;
        public List<Notification> NotificationList
        {
            get
            {
                return _notificationList;
            }
            set
            {
                if (_notificationList == null)
                {
                    _notificationList = value;
                }
                else lock (syncRoot)
                {
                    _notificationList = value;
                }
            }
        }

        //info about post lists
        //TODO: pack comments with Post
        //either fix lazy loading or make request
        private List<Post> _newsFeedPostList;
        public List<Post> NewsfeedPostList
        {
            get
            {
                return _newsFeedPostList;
            }
            set
            {
                if (_newsFeedPostList == null)
                {
                    _newsFeedPostList = value;
                }
                else lock (syncRoot)
                {
                    _newsFeedPostList = value;
                }
            }
        }

        private List<Post> _currentProfilePostList;
        public List<Post> currentProfilePostList
        {
            get
            {
                return _currentProfilePostList;
            }
            set
            {
                if (_currentProfilePostList == null)
                {
                    _currentProfilePostList = value;
                }
                else lock (syncRoot)
                {
                    _currentProfilePostList = value;
                }
            }
        }

        //info about new messages
        private List<UnreadMessage> _unreadMessagesList;
        public List<UnreadMessage> UnreadMessagesList
        {
            get
            {
                return _unreadMessagesList;
            }
            set
            {
                if (_unreadMessagesList == null)
                {
                    _unreadMessagesList = value;
                }
                else lock (syncRoot)
                {
                    _unreadMessagesList = value;
                }
            }
        }
        

    }
}
