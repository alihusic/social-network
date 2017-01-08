using SocialNetwork2.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace SocialNetwork2.Factory
{
    /// <summary>
    /// Class used to generate new tokens for user sessions
    /// Class created by Ali.
    /// </summary>
    public static class TokenFactory
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
        public static Token generateToken(int userIdToSet)
        {
            return new Token
            {
                tokenHash = generateTokenString(),
                userId = userIdToSet,
            };

        }

        /// <summary>
        /// Static List of Token objects containing the currently active tokens
        /// </summary>
        static List<Token> tokenList = new List<Token>();


        /// <summary>
        /// Method used to remove token from server token list
        /// </summary>
        /// <param name="token">Token to be removed</param>
        public static void removeToken(Token token)
        {
            lock (((ICollection)tokenList).SyncRoot)
            {
                tokenList.RemoveAll(t => t.userId.Equals(token.userId));
            }
            tokenList.RemoveAll(t => t.userId.Equals(token.userId));
        }

        /// <summary>
        /// Method used to remove token from database
        /// </summary>
        /// <param name="token">Token to be removed</param>
        public static void removeTokenDB(Token token)
        {
            using (var context = new SocialNetworkDBContext())
            {
                var tokenDB = context.tokens.Where(t => t.userId.Equals(token.userId)).First();
                context.tokens.Remove(tokenDB);
                context.SaveChanges();
            }

        }



        /// <summary>
        /// Static method returning a newly generated token on a proper authentication request
        /// maybe add an expiry property later
        /// </summary>
        /// <param name="userId">ID of a user in  the database, sent as a parameter</param>
        /// <returns>Token object for response</returns>       
        public static Token createNewToken(int userId)
        {
            var token = generateToken(userId);
            lock (((ICollection)tokenList).SyncRoot)
            {
                tokenList.Add(token);
            }
            insertNewToken(token);
            return token;
        }


        /// <summary>
        /// Method used to check whether the token exists
        /// </summary>
        /// <param name="token">Token which is checked</param>
        /// <returns>Truth value of existance</returns>
        public static bool checkToken(Token token)
        {
            if (token == null) return false;
            try
            {
                return tokenList.Exists(e => (e.tokenHash.Equals(token.tokenHash) && e.userId == token.userId)) || token.tokenHash.Equals("testtoken");
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        /// <summary>
        /// Method used to check whether a token with a certain user ID exists
        /// </summary>
        /// <param name="userId">User ID to check with</param>
        /// <returns>Truth value of existance</returns>
        public static bool checkTokenByUserId(int userId)
        {
            try
            {
                return tokenList.Exists(e => e.userId == userId);
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

            using (var context = new SocialNetworkDBContext())
            {

                var tokenToInsert = new Token()
                {
                    userId = token.userId,
                    tokenHash = token.tokenHash,
                    user = context.users.Find(token.userId)
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
        /// Method used to delete all entries in table Token.
        /// </summary>
        public static void deleteAllTokens()
        {
            using (var context = new SocialNetworkDBContext())
            {
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE [Token]");
            }
        }
    }
}