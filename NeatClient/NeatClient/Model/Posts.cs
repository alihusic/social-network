using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Model
{

    //model for Posts table
    //used for storage of posts 

    public class Posts
    {
        public int postsId { get; set; }
        public string postContent { get; set; }
        public DateTime postCreationDate { get; set; }
        public int creatorId { get; set; }
        public int targetId { get; set; }
        public int numOfLikes { get; set; }
    }
}
