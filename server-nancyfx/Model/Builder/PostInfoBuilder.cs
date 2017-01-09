using SocialNetwork2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork2.Model.Builder
{
    /// <summary>
    /// Class created by Ermin & Ali.
    /// </summary>
    public class PostInfoBuilder
    {
        public int postId { get; set; }
        public int userId { get; set; }
        public string commentText { get; set; }

        public PostInfoBuilder PostId(int postId)
        {
            this.postId = postId;
            return this;
        }

        public PostInfoBuilder UseriId(int userId)
        {
            this.userId = userId;
            return this;
        }

        public PostInfoBuilder CommentText(string commentText)
        {
            this.commentText = commentText;
            return this;
        }


        public PostInfo Build()
        {
            return new PostInfo()
            {
                postId = postId,
                userId = userId,
                commentText = commentText
            };
        }
    }
}