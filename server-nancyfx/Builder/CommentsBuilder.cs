using SocialNetwork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkServer.Builder
{
    class CommentsBuilder
    {
        private int postId;
        private int userId;
        private DateTime commentTimeStamp;
        private string commentText;


        public CommentsBuilder PostId(int postId)
        {
            this.postId = postId;
            return this;
        }


        public CommentsBuilder UserId(int userId)
        {
            this.userId = userId;
            return this;
        }


        public CommentsBuilder CommentTimeStamp(DateTime commentTimeStamp)
        {
            this.commentTimeStamp  = commentTimeStamp;
            return this;
        }


        public CommentsBuilder CommentText(string commentText)
        {
            this.commentText = commentText;
            return this;
        }

        public Comments Build()
        {
            return new Comments()
            {
                postId = postId,
                userId = userId,
                commentTimeStamp = commentTimeStamp,
                commentText = commentText
            };
        }
    }
}
