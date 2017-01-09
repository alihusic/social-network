using SocialNetwork2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork2.Response
{
    public class PostResponse:StatusResponse
    {
        public Post post { get; set; }
        public PostResponse(Post post) : base(true)
        {
            this.post = post;
        }
    }
}