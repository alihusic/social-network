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
    class LikeBuilder
    {
        private int userId;
        private int postId;


        public LikeBuilder UserId(int userId)
        {
            this.userId = userId;
            return this;
        }

        public LikeBuilder PostId(int postId)
        {
            this.postId = postId;
            return this;
        }

        public Like Build()
        {
            return new Like()
            {
                userId = userId,
                postId = postId
            };
        }
    }
}
