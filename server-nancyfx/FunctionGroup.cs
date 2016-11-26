using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocialNetwork.Model;
using System.Security.Cryptography;

namespace SocialNetworkServerNV1
{
    //TODO: RENAME COOKIES TO TOKENS
    /// <summary>
    /// Interface used to enrich CookieFactory/ies
    /// </summary>
    interface ICreateCookies
    {
        Cookie generateCookie(int userId);
    }

    /// <summary>
    /// Class used to generate new cookies for user sessions
    /// </summary>
    public class CookieFactory : ICreateCookies
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
        private static string generateCookieString()
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

        //generateCookie() returns a randomly generated cookie
        /// <summary>
        /// Method used to return a Cookie for response
        /// </summary>
        /// <param name="userIdToSet"></param>
        /// <returns>Randomly generated Cookie from the randomly generated cookie string</returns>

        public Cookie generateCookie(int userIdToSet)
        {
            return new Cookie
            {
                cookieHash = generateCookieString(),
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
        /// Static List of Cookie objects containing the currently active cookies
        /// </summary>
        static List<Cookie> cookieList = new List<Cookie>();

        /// <summary>
        /// Static instance of a CookieFactory
        /// </summary>
        static CookieFactory cookieFactory = new CookieFactory();
        
        /// <summary>
        /// Static method returning a newly generated cookie on a proper authentication request
        /// maybe add an expiry property later
        /// </summary>
        /// <param name="userId">ID of a user in  the database, sent as a parameter</param>
        /// <returns>Cookie object for response</returns>       
        static Cookie createNewCookie(int userId)
        {
            var cookie = cookieFactory.generateCookie(userId);
            cookieList.Add(cookie);
            insertNewCookie(cookie);
            return cookie;
        }

        /// <summary>
        /// Inserts a new cookie into database
        /// </summary>
        /// <param name="cookie">a Cookie generated from the factory</param>
        private static void insertNewCookie(Cookie cookie)
        {
            using (var context = new SocialNetworkDBContext())
            {
                var cookieToInsert = new Cookie()
                {
                    userId=cookie.userId,
                    cookieHash=cookie.cookieHash
                };

                context.cookies.Add(cookieToInsert);
                context.SaveChanges();
            }
        }

    }
} 