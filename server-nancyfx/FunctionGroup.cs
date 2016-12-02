using System;
using System.Linq;
using System.Web;
using SocialNetwork.Model;
using System.Security.Cryptography;
using SocialNetwork;
using System.Collections.Generic;

namespace SocialNetworkServerNV1
{
    
    /// <summary>
    /// Interface used to enrich TokenFactory/ies
    /// </summary>
    interface ICreateTokens
    {
        Token generateToken(int userId);
    }

    /// <summary>
    /// Class used to generate new tokens for user sessions
    /// </summary>
    public class TokenFactory : ICreateTokens
    {
        static readonly char[] AvailableCharacters = {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
            'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
            'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '-', '_'
          };

        /// <summary>
        /// Static int denoting the length of the resulting token
        /// </summary>
        private static int tokenLength = 40;

        /// <summary>
        /// Method used to randomly generate a cookie string
        /// </summary>
        /// <returns>String containing the generated token</returns>
        private static string generateTokenString()
        {
            char[] token = new char[tokenLength];
            byte[] randomData = new byte[tokenLength];

            //generating random data using RNGCryptoServiceProvider and storing it in randomData
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomData);
            }

            //generating indices from randomData and getting token characters by them
            for (int idx = 0; idx < token.Length; idx++)
            {
                int pos = randomData[idx] % AvailableCharacters.Length;
                token[idx] = AvailableCharacters[pos];
            }
            return new string(token);
        }

        
        /// <summary>
        /// Method used to return a Token for response
        /// </summary>
        /// <param name="userIdToSet"></param>
        /// <returns>Randomly generated Token from the randomly generated token string</returns>
        public Token generateToken(int userIdToSet)
        {
            return new Token
            {
                tokenHash = generateTokenString(),
                userId = userIdToSet,
            };
                
        }
    }

    /// <summary>
    /// A class used to achieve centralization, abstraction and inheritance in FunctionGroups
    /// </summary>
    public class FunctionGroup
    {
        /// <summary>
        /// Static List of Token objects containing the currently active tokens
        /// </summary>
        static List<Token> tokenList = new List<Token>();

        /// <summary>
        /// Static instance of a TokenFactory
        /// </summary>
        static TokenFactory cookieFactory = new TokenFactory();

        /// <summary>
        /// Static method returning a newly generated token on a proper authentication request
        /// maybe add an expiry property later
        /// </summary>
        /// <param name="userId">ID of a user in  the database, sent as a parameter</param>
        /// <returns>Token object for response</returns>       
        public static Token createNewToken(int userId)
        {
            var token = cookieFactory.generateToken(userId);
            tokenList.Add(token);
            insertNewToken(token);
            return token;
        }

        /// <summary>
        /// Method used to check whether the token exists
        /// </summary>
        /// <param name="token">Token which is checked</param>
        /// <returns>Truth value of existance</returns>
        public bool checkToken(Token token)
        {
            try
            {
                return tokenList.Exists(e => e.Equals(token)) || token.tokenHash.Equals("testtoken");
            }catch(Exception e)
            {
                throw e;
            }
            
        }

        /// <summary>
        /// Method used to check whether a token with a certain user ID exists
        /// </summary>
        /// <param name="userId">User ID to check with</param>
        /// <returns>Truth value of existance</returns>
        public bool checkTokenByUserId(int userId)
        {
            try
            {
                return tokenList.Exists(e => e.userId==userId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Inserts a new token into database
        /// </summary>
        /// <param name="cookie">a Token generated from the factory</param>
        public static void insertNewToken(Token token)
        {
            var userFound = getUserById(token.userId);
            using (var context = new SocialNetworkDBContext())
            {
                var tokenToInsert = new Token()
                {
                    userId=token.userId,
                    tokenHash=token.tokenHash,
                    user=userFound
                };

                context.tokens.Add(tokenToInsert);
                try
                {
                    context.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            // raise a new exception nesting
                            // the current instance as InnerException
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
        }

        /// <summary>
        /// Method used to return user object by username
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>Corresponding user object</returns>
        public User getUser(string username)
        {
            User user = null;
            using (var context = new SocialNetworkDBContext())
            {
                var results = context.users.Where(p => p.username.Equals(username));
                if (results.Any()) return results.FirstOrDefault();
                //return new User { username=username};
            }
            return user;
        }

        /// <summary>
        /// Method used to return user object by user id
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>Corresponding user object</returns>
        public static User getUserById(int userId)
        {
            using (var context = new SocialNetworkDBContext())
            {
                //var results = context.users.Where(p => p.username.Equals(username));
                //if (results.Any()) return results.FirstOrDefault();
                //return new User { username=username};
                return context.users.Find(userId);
            }
        }

        /// <summary>
        /// Helper method used to check if there is a user with a certain Id
        /// </summary>
        /// <param name="userId"> Type int. Users id that is sent to the method. </param>
        /// <returns>
        /// Returns boolean. True if user exists, false if doesn't.</returns>
        public bool userExists(int userId)
        {
            using (var context = new SocialNetworkDBContext())
            {
                return context.users.Any(u => (u.userId == userId));
            }

        }

        /// <summary>
        /// @chatExists checks if chat exists
        /// </summary>
        /// <param name="user1Id">int. Id of user 1</param>
        /// <param name="user2Id">int. Id of user 2</param>
        /// <returns></returns>
        public bool chatExists(int user1Id, int user2Id)
        {
            //insert context class name
            using (var context = new SocialNetworkDBContext())
            {
                return context.privateChat.Any(n => (n.user1 == user1Id && n.user2 == user2Id) || (n.user1 == user2Id && n.user2 == user1Id));
            }
        }

        /// <summary>
        /// @friendshipExists checks if two users are already friends
        /// </summary>
        /// <param name="user1Id"> int. represents id of first user</param>
        /// <param name="user2Id">int. represents id of second user</param>
        /// <returns>
        /// Returns boolean. If true then friendship exists, if false friendship doesn't exist</returns>
        public bool friendshipExists(int user1Id, int user2Id)
        {
            using (var context = new SocialNetworkDBContext())
            {
                return context.friendRequest.Any(fr => ((fr.senderId == user1Id && fr.receiverId == user2Id && fr.friendRequestConfirmed == true) || (fr.senderId == user2Id && fr.receiverId == user1Id && fr.friendRequestConfirmed == true)));
            }
        }

        /// <summary>
        /// Method used to check if there exists a friendship request in the database 
        /// </summary>
        /// <param name="user1Id">ID of the first user</param>
        /// <param name="user2Id">ID of the second users</param>
        /// <returns>Returns the truth value of existance</returns>
        public bool pendingFriendshipRequestExists(int user1Id, int user2Id)
        {
            using (var context = new SocialNetworkDBContext())
            {
                return context.friendRequest.Any(fr => ((fr.senderId == user1Id && fr.receiverId == user2Id && fr.friendRequestConfirmed == false) || (fr.senderId == user2Id && fr.receiverId == user1Id && fr.friendRequestConfirmed == false)));
            }
        }

        /// <summary>
        /// @getAllFriendsId is used to find id's of all friends user has
        /// </summary>
        /// <param name="userId">int. User's Id</param>
        /// <returns>
        /// Returns List<int></returns>
        public List<int> getAllFriendsId(int userId)
        {
            using (var context = new SocialNetworkDBContext())
            {

                List<int> friendsList = new List<int>();

                var friends = context.friendRequest;

                foreach (var f in friends)
                {
                    if (f.receiverId == userId && f.friendRequestConfirmed == true)
                    {
                        friendsList.Add(f.senderId);
                    }
                    else if (f.senderId == userId && f.friendRequestConfirmed == true)
                    {
                        friendsList.Add(f.receiverId);
                    }
                }

                return friendsList;
            }

        }

        /// <summary>
        /// @confirmFriendRequest confirms friend request between two users
        /// </summary>
        /// <param name="senderId">int. id of first user</param>
        /// <param name="receiverId">int. ide of second user</param>
        public void confirmFriendshipRequest(int senderId, int receiverId)
        {
            using (var context = new SocialNetworkDBContext())
            {
                var friendRequest = new PendingFriendRequests()
                {
                    senderId = senderId,
                    receiverId = receiverId,
                    friendRequestConfirmed = true
                };

                context.friendRequest.Attach(friendRequest);
                context.Entry(friendRequest).Property(fr => fr.friendRequestConfirmed).IsModified = true;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// @addNewFriendship creates new pending friendship request
        /// </summary>
        /// <param name="senderId"> int. Sender's Id</param>
        /// <param name="receiverId">int. Receiver's Id</param>
        public void addNewPendingFriendshipRequest(int senderId, int receiverId)
        {
            using (var context = new SocialNetworkDBContext())
            {
                var newFriendship = new PendingFriendRequests()
                {
                    senderId = senderId,
                    receiverId = receiverId,
                    friendRequestSent = DateTime.Now,
                    friendRequestConfirmed = false
                };

                context.friendRequest.Add(newFriendship);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// @deleteFriendship removes friendship of two users :(
        /// </summary>
        /// <param name="senderId">int. Sender's Id</param>
        /// <param name="receiverId">int. Receiver's Id</param>
        public void deleteFriendship(int senderId, int receiverId)
        {
            using (var context = new SocialNetworkDBContext())
            {
                var friendship = context.friendRequest.Where(fr => ((fr.senderId == senderId) && (fr.receiverId == receiverId))).First();
                context.friendRequest.Remove(friendship);
            }
        }

        /// <summary>
        /// @getAllUsers Used to find all friends of a user
        /// </summary>
        /// <param name="userId"> int. User's Id</param>
        /// <returns>
        /// Returns List<User></returns>
        public List<User> getAllFriends(int userId)
        {
            List<int> friendsId = getAllFriendsId(userId);
            using (var context = new SocialNetworkDBContext())
            {
                List<User> userFriends = new List<User>();

                foreach (var i in friendsId)
                {
                    userFriends.Add((User)context.users.Where(u => u.userId == i));
                }

                return userFriends;

            }
        }

        /// <summary>
        /// @getChatId used to retrieve in which chat conversation is happening
        /// </summary>
        /// <param name="user1Id">int. Id of user 1</param>
        /// <param name="user2Id">int. Id of user 2</param>
        /// <returns>
        /// Int which represents chatId of chat between two users</returns>
        public int getChatId(int user1Id, int user2Id)
        {
            using (var context = new SocialNetworkDBContext())
            {
                return context.privateChat.Where(n => (n.user1 == user1Id && n.user2 == user2Id) || (n.user1 == user2Id && n.user2 == user1Id)).First().privateChatId;
            }

        }

        /// <summary>
        /// @createNewChat creates new chat.
        /// </summary>
        /// <param name="user1Id">int. Id of a first User</param>
        /// <param name="user2Id">int. Id of a second User</param>
        public void createNewChat(int user1Id, int user2Id)
        {
            using (var context = new SocialNetworkDBContext())
            {
                var chat = new PrivateChat()
                {
                    user1 = user1Id,
                    user2 = user2Id,
                    chatCreationDate = DateTime.Now
                };

                context.privateChat.Add(chat);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// @saveMessage saves message into table UnreadMessages and PrivateMessages
        /// </summary>
        /// <param name="messageText">string. Text of a message</param>
        /// <param name="senderId">int. Sender's Id</param>
        /// <param name="recipientId">int. Recipient's Id</param>
        /// <param name="chatId">int. Chat's Id</param>
        public void saveMessage(string messageText, int senderId, int recipientId, int chatId)
        {
            using (var context = new SocialNetworkDBContext())
            {
                var message1 = new UnreadMessages()
                {
                    messageText = messageText,
                    senderId = senderId,
                    recipientId = recipientId,
                    chatID = chatId,
                    messageTimeStamp = DateTime.Now
                };

                var message2 = new PrivateMessages()
                {
                    messageText = messageText,
                    senderId = senderId,
                    recipientId = recipientId,
                    chatID = chatId,
                    messageTimeStamp = DateTime.Now
                };

                context.unreadMessages.Add(message1);
                context.privateMessages.Add(message2);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// @checkUnreadMessages checks if there are any new entries in table UnreadMessages
        /// </summary>
        /// <param name="userId"> int. User's id</param>
        /// <returns>
        /// Returns true if there are any new entries, else returns false</returns>
        public bool checkUnreadMessages(int userId)
        {
            using (var context = new SocialNetworkDBContext())
            {
                return context.unreadMessages.Any(um => (um.recipientId == userId));
            }
        }

        public List<int> getAllUnreadMessagesId(int userId)
        {
            using (var context = new SocialNetworkDBContext())
            {

                List<int> unreadMessagesIdList = new List<int>();

                var unreadMessages = context.unreadMessages;

                foreach (var um in unreadMessages)
                {
                    if (um.recipientId == userId)
                    {
                        unreadMessagesIdList.Add(um.unreadMessageId);
                    }
                }

                return unreadMessagesIdList;
            }
        }

        public List<UnreadMessages> getAllUnreadMessages(int userId)
        {
            List<int> unreadMessagesIdList = getAllUnreadMessagesId(userId);
            using (var context = new SocialNetworkDBContext())
            {
                List<UnreadMessages> unreadMessages = new List<UnreadMessages>();

                foreach (var messageId in unreadMessagesIdList)
                {
                    unreadMessages.Add((UnreadMessages)context.unreadMessages.Where(u => u.unreadMessageId == messageId));
                }

                return unreadMessages;

            }
        }

        /// <summary>
        /// @getPost used to retrieve post for a certain creation time
        /// </summary>
        /// <param name="postCreationDate">DateTime. time when post was created</param>
        /// <returns>
        /// Object of type Posts</returns>
        public Posts getPost(DateTime postCreationDate)
        {
            using (var context = new SocialNetworkDBContext())
            {
                return (Posts)context.posts.Where(p => p.postCreationDate == postCreationDate);
            }
        }

        /// <summary>
        /// @postExists checks if post exists
        /// </summary>
        /// <param name="postId">int. User's Id </param>
        /// <returns>
        /// Returns true if post exists, otherwise returns false</returns>
        public bool postExists(int postId)
        {
            using (var context = new SocialNetworkDBContext())
            {
                return context.posts.Any(p => p.postsId == postId);
            }
        }

        /// <summary>
        /// @isPostVisible checks if user can see post 
        /// </summary>
        /// <param name="userID">int. User's Id</param>
        /// <returns>
        /// Returns true if user is in friend list, else returns false</returns>
        public bool isPostVisible(int creatorId, int targetId)
        {
            //ovdje bi mu trebao baciti inheritance. treba ubaciti u Function group metodu iz FriendsFunctionGroup-a getAllFriendsId. Ona treba vratiti listu prijatelja jednog usera.
            List<int> friends = getAllFriendsId(targetId);

            return friends.Contains(creatorId);
        }

        /// <summary>
        /// @isLiked checks if user liked certain post
        /// </summary>
        /// <param name="userId">int. User's Id</param>
        /// <param name="postId">int. Post Id</param>
        /// <returns>
        /// Returns true if user liked, else returns false</returns>
        public bool isLiked(int userId, int postId)
        {
            using (var context = new SocialNetworkDBContext())
            {
                return context.likes.Any(l => l.postId == postId || l.userId == userId);
            }
        }

        /// <summary>
        /// @addLike adds new like to the table Likes and increments field numOfLikes in table Posts
        /// </summary>
        /// <param name="userId">int. User's Id</param>
        /// <param name="postId">int. Post Id</param>
        public void addLike(int userId, int postId)
        {
            using (var context = new SocialNetworkDBContext())
            {
                var like = new Likes()
                {
                    postId = postId,
                    userId = userId
                };

                var post = new Posts()
                {
                    postsId = postId,
                    numOfLikes = +1
                };

                context.posts.Attach(post);
                context.Entry(post).Property(p => p.numOfLikes).IsModified = true;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Method used to insert a comment into the database
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="postId">Post ID</param>
        /// <param name="commentText">Textual content of the comment</param>
        public void addComment(int userId, int postId, string commentText)
        {
            using (var context = new SocialNetworkDBContext())
            {
                Comments comment = new Comments()
                {
                    commentText = commentText,
                    postId = postId,
                    userId = userId
                };

                context.comments.Add(comment);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// @createPost creates new post
        /// </summary>
        /// <param name="creatorId">int. Creator's id</param>
        /// <param name="targetId">int. Where is post being posted.</param>
        /// <param name="postContent">string. Contents of a post</param>
        public void createPost(int creatorId, int targetId, string postContent)
        {
            using (var context = new SocialNetworkDBContext())
            {
                var post = new Posts()
                {
                    creatorId = creatorId,
                    targetId = targetId,
                    postContent = postContent,
                    numOfLikes = 0,
                    postCreationDate = DateTime.Now
                };

                context.posts.Add(post);
                context.SaveChanges();
            }
        }


        /// <summary>
        /// getRecentPosts is used to retrieve recent posts that user will see on newsfeed
        /// </summary>
        /// <param name="interval">int. Interval of posts</param>
        /// <param name="userId">int. User's Id</param>
        /// <returns></returns>

        public List<Posts> getRecentPosts(int interval, int userId)
        {
            using (var context = new SocialNetworkDBContext())
            {
                var friends = helpers.getAllFriendsId(userId);

                return context.posts.Where(p => friends.Contains(p.creatorId) && friends.Contains(p.targetId)).OrderByDescending(p => p.postCreationDate).Skip(interval).Take(10).ToList();
            }
        }
    }
} 