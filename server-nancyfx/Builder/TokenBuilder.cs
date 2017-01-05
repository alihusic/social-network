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
    class TokenBuilder
    {
        private int userId;
        private string tokenHash;


        public TokenBuilder UserId(int userId)
        {
            this.userId = userId;
            return this;
        }


        public TokenBuilder TokenHash(string tokenHash)
        {
            this.tokenHash = tokenHash;
            return this;
        }

        public Token Build()
        {
            return new Token()
            {
                userId = userId,
                tokenHash = tokenHash
            };
        }
    }
}
