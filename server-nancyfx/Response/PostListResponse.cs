using SocialNetwork2.Model;
using System.Collections.Generic;

namespace SocialNetwork2.Response
{
    public class PostListResponse:StatusResponse
    {
        public List<Post> postList { get; set; }
        public PostListResponse(List<Post> postList ) :base(true)
        {
            this.postList = postList;
        }
    }
}