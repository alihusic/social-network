using SocialNetwork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkServer.Builder
{
    class PostBuilder
    {
        private string postContent;
        private DateTime postCreationTimeStamp;
        private int creatorId;
        private int targetId;
        private int numOfLikes;

        public PostBuilder PostContent(string postContent)
        {
            this.postContent = postContent;
            return this;
        }

        public PostBuilder PostCreationDate(DateTime postCreationDate)
        {
            this.postCreationTimeStamp = postCreationDate;
            return this;
        }


        public PostBuilder CreatorId(int creatorId)
        {
            this.creatorId = creatorId;
            return this;
        }

        public PostBuilder TargetId(int targetId)
        {
            this.targetId = targetId;
            return this;
        }

        public PostBuilder NumOfLikes(int numOfLikes)
        {
            this.numOfLikes = numOfLikes;
            return this;
        }


        public Post Build()
        {
            return new Post()
            {
                postContent = postContent,
                postCreationTimeStamp = postCreationTimeStamp,
                creatorId = creatorId,
                targetId = targetId,
                numOfLikes = numOfLikes
            };
        }
    }
}
