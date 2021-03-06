﻿using SocialNetwork2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork2.Model.Builder
{

    /// <summary>
    /// Class created by Ermin & Ali.
    /// </summary>
    class CommentBuilder
    {
        private int postId;
        private int userId;
        private DateTime commentTimeStamp;
        private string commentText;


        public CommentBuilder PostId(int postId)
        {
            this.postId = postId;
            return this;
        }


        public CommentBuilder UserId(int userId)
        {
            this.userId = userId;
            return this;
        }


        public CommentBuilder CommentTimeStamp(DateTime commentTimeStamp)
        {
            this.commentTimeStamp  = commentTimeStamp;
            return this;
        }


        public CommentBuilder CommentText(string commentText)
        {
            this.commentText = commentText;
            return this;
        }

        public Comment Build()
        {
            return new Comment()
            {
                postId = postId,
                userId = userId,
                commentTimeStamp = commentTimeStamp,
                commentText = commentText
            };
        }
    }
}
