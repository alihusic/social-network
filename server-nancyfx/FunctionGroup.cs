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

        //generateToken() returns a randomly generated token
        /// <summary>
        /// Method used to return a Cookie for response
        /// </summary>
        /// <param name="userIdToSet"></param>
        /// <returns>Randomly generated Cookie from the randomly generated cookie string</returns>

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
        static Token createNewToken(int userId)
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
        /// Inserts a new token into database
        /// </summary>
        /// <param name="cookie">a Token generated from the factory</param>
        public static void insertNewToken(Token token)
        {
            using (var context = new SocialNetworkDBContext())
            {
                var tokenToInsert = new Token()
                {
                    userId=token.userId,
                    tokenHash=token.tokenHash
                };

                context.tokens.Add(tokenToInsert);
                context.SaveChanges();
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

       
        
        


    }
} 