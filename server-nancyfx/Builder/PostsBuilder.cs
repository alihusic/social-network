using SocialNetwork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkServer.Builder
{
    class PostsBuilder
    {
        private string postContent;
        private DateTime postCreationDate;
        private int creatorId;
        private int targetId;
        private int numOfLikes;

        public PostsBuilder PostContent(string postContent)
        {
            this.postContent = postContent;
            return this;
        }

        public PostsBuilder PostCreationDate(DateTime postCreationDate)
        {
            this.postCreationDate = postCreationDate;
            return this;
        }


        public PostsBuilder CreatorId(int creatorId)
        {
            this.creatorId = creatorId;
            return this;
        }

        public PostsBuilder TargetId(int targetId)
        {
            this.targetId = targetId;
            return this;
        }

        public PostsBuilder NumOfLikes(int numOfLikes)
        {
            this.numOfLikes = numOfLikes;
            return this;
        }


        public Posts Build()
        {
            return new Posts()
            {
                postContent = postContent,
                postCreationDate = postCreationDate,
                creatorId = creatorId,
                targetId = targetId,
                numOfLikes = numOfLikes
            };
        }
    }
}
