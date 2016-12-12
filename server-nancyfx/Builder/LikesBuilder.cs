<<<<<<< HEAD
﻿using SocialNetwork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkServer.Builder
{
    class LikesBuilder
    {
        private int userId;
        private int postId;


        public LikesBuilder UserId(int userId)
        {
            this.userId = userId;
            return this;
        }

        public LikesBuilder PostId(int postId)
        {
            this.postId = postId;
            return this;
        }

        public Likes Build()
        {
            return new Likes()
            {
                userId = userId,
                postId = postId
            };
        }
    }
}
=======
﻿using SocialNetwork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkServer.Builder
{
    class LikesBuilder
    {
        private int userId;
        private int postId;


        public LikesBuilder UserId(int userId)
        {
            this.userId = userId;
            return this;
        }

        public LikesBuilder PostId(int postId)
        {
            this.postId = postId;
            return this;
        }

        public Likes Build()
        {
            return new Likes()
            {
                userId = userId,
                postId = postId
            };
        }
    }
}
>>>>>>> refs/remotes/origin/Maulwurf
