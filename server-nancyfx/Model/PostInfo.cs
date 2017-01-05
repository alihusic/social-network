using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork2.Model
{
    /// <summary>
    /// Class used as model for information about posts.
    /// Class created by Ermin.
    /// </summary>
    public class PostInfo
    {
        public int postId { get; set; }
        public int userId { get; set; }
        public string commentText { get; set; }
    }
}