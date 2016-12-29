using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork2.Model
{
    public class PostInfo
    {
        public int postId { get; set; }
        public int userId { get; set; }
        public string commentText { get; set; }
    }
}