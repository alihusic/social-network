using SocialNetwork2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetworkServerNV1.Response
{
    public class CommentListResponse:StatusResponse
    {
        public List<Comment> commentList { get; set; }
        public CommentListResponse(List<Comment> commentList) : base(true)
        {
            this.commentList = commentList;
        }
    }
}